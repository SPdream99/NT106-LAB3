using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;

namespace Lab03_Bai04_Server
{
    internal class Program
    {
        // Danh sách ghế và danh sách client đang kết nối
        private static readonly List<Seat> Seats = new List<Seat>();
        private static readonly List<ClientHandler> Clients = new List<ClientHandler>();
        private static readonly object _lock = new object();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            InitSeats(); // tạo sẵn vài ghế mẫu

            int port = 8080;
            var listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            Console.WriteLine($"Server started on port {port}. Waiting for clients...");

            while (true)
            {
                // chấp nhận client mới
                TcpClient tcpClient = listener.AcceptTcpClient();
                Console.WriteLine("New client connected.");

                var handler = new ClientHandler(tcpClient);
                lock (_lock)
                {
                    Clients.Add(handler);
                }

                var t = new Thread(handler.Run) { IsBackground = true };
                t.Start();
            }
        }

        /// <summary>
        /// Khởi tạo 20 ghế mặc định (1..20), trạng thái Free.
        /// Nếu bạn muốn đọc từ file thì sửa hàm này.
        /// </summary>
        private static void InitSeats()
        {
            for (int i = 1; i <= 20; i++)
            {
                Seats.Add(new Seat
                {
                    Id = i,
                    IsBooked = false,
                    BookedBy = ""
                });
            }
        }

        /// <summary>
        /// Lấy ghế theo id (đã lock).
        /// </summary>
        internal static Seat? GetSeat(int id)
        {
            lock (_lock)
            {
                return Seats.FirstOrDefault(s => s.Id == id);
            }
        }

        /// <summary>
        /// Broadcast trạng thái ghế tới tất cả client.
        /// Gửi: UPDATE id status bookedBy
        /// </summary>
        internal static void BroadcastSeat(Seat seat)
        {
            string msg = $"UPDATE {seat.Id} {seat.Status} {seat.BookedBy}".TrimEnd();

            lock (_lock)
            {
                foreach (var c in Clients.ToList())
                {
                    try { c.Send(msg); }
                    catch { /* bỏ qua client lỗi */ }
                }
            }

            Console.WriteLine("Broadcast: " + msg);
        }

        /// <summary>
        /// Xóa client khi ngắt kết nối.
        /// </summary>
        internal static void RemoveClient(ClientHandler handler)
        {
            lock (_lock)
            {
                Clients.Remove(handler);
            }
        }

        /// <summary>
        /// Gửi toàn bộ danh sách ghế cho 1 client (khi client gửi LIST).
        /// Mỗi ghế: SEAT id status bookedBy
        /// </summary>
        internal static void SendAllSeats(ClientHandler handler)
        {
            lock (_lock)
            {
                foreach (var s in Seats)
                {
                    string line = $"SEAT {s.Id} {s.Status} {s.BookedBy}".TrimEnd();
                    handler.Send(line);
                }
            }
        }
    }

    internal class ClientHandler
    {
        private readonly TcpClient _client;
        private readonly StreamReader _reader;
        private readonly StreamWriter _writer;
        private bool _running = true;

        public ClientHandler(TcpClient client)
        {
            _client = client;
            NetworkStream ns = client.GetStream();
            _reader = new StreamReader(ns, Encoding.UTF8);
            _writer = new StreamWriter(ns, Encoding.UTF8) { AutoFlush = true };
        }

        public void Send(string msg)
        {
            _writer.WriteLine(msg);
        }

        public void Run()
        {
            try
            {
                while (_running)
                {
                    string? line = _reader.ReadLine();
                    if (line == null) break;

                    Console.WriteLine("Received: " + line);
                    HandleCommand(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Client error: " + ex.Message);
            }
            finally
            {
                _client.Close();
                Program.RemoveClient(this);
                Console.WriteLine("Client disconnected.");
            }
        }

        private void HandleCommand(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return;

            var parts = line.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
            string cmd = parts[0].ToUpperInvariant();

            switch (cmd)
            {
                case "LIST":
                    Program.SendAllSeats(this);
                    break;

                case "BOOK":
                    if (parts.Length < 3)
                    {
                        Send("ERR Invalid BOOK command");
                        return;
                    }
                    HandleBook(parts[1], parts[2]);
                    break;

                case "CANCEL":
                    if (parts.Length < 3)
                    {
                        Send("ERR Invalid CANCEL command");
                        return;
                    }
                    HandleCancel(parts[1], parts[2]);
                    break;

                case "QUIT":
                    Send("OK QUIT");
                    _running = false;
                    break;

                default:
                    Send("ERR Unknown command");
                    break;
            }
        }

        private void HandleBook(string idStr, string username)
        {
            if (!int.TryParse(idStr, out int id))
            {
                Send("ERR Invalid seat id");
                return;
            }

            Seat? seat = Program.GetSeat(id);
            if (seat == null)
            {
                Send("ERR Seat not found");
                return;
            }

            lock (seat)
            {
                if (seat.IsBooked && !string.Equals(seat.BookedBy, username, StringComparison.OrdinalIgnoreCase))
                {
                    Send("ERR Seat already booked");
                    return;
                }

                seat.IsBooked = true;
                seat.BookedBy = username;

                Send($"OK BOOK {seat.Id}");
                Program.BroadcastSeat(seat);
            }
        }

        private void HandleCancel(string idStr, string username)
        {
            if (!int.TryParse(idStr, out int id))
            {
                Send("ERR Invalid seat id");
                return;
            }

            Seat? seat = Program.GetSeat(id);
            if (seat == null)
            {
                Send("ERR Seat not found");
                return;
            }

            lock (seat)
            {
                if (!seat.IsBooked)
                {
                    Send("ERR Seat not booked");
                    return;
                }

                // Nếu muốn chỉ người đã đặt mới được hủy thì check thêm:
                // if (!string.Equals(seat.BookedBy, username, StringComparison.OrdinalIgnoreCase)) ...

                seat.IsBooked = false;
                seat.BookedBy = "";

                Send($"OK CANCEL {seat.Id}");
                Program.BroadcastSeat(seat);
            }
        }
    }
}
