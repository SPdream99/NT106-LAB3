using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Lab03_Bai04Server
{
    class Seat
    {
        public int Id { get; set; }
        public bool IsBooked { get; set; }
        public string BookedBy { get; set; } = "";
        public string Status => IsBooked ? "Booked" : "Free";
    }

    class Program
    {
        static List<Seat> Seats = new List<Seat>();
        static List<ClientHandler> Clients = new List<ClientHandler>();
        static object _lock = new object();

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            // init seats 1..10
            for (int i = 1; i <= 10; i++)
            {
                Seats.Add(new Seat { Id = i, IsBooked = false });
            }

            int port = 8080;
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            //listener.Start();

            Console.WriteLine($"SERVER started on port {port}. Waiting for clients...");

            while (true)
            {
                //TcpClient tcp = listener.AcceptTcpClient();
                Console.WriteLine("New client connected.");

                ClientHandler handler = new ClientHandler(tcp);
                lock (_lock) Clients.Add(handler);

                Thread t = new Thread(handler.Run) { IsBackground = true };
                t.Start();
            }
        }

        public static void Broadcast(Seat s)
        {
            string msg = $"UPDATE {s.Id} {s.Status} {s.BookedBy}";
            lock (_lock)
            {
                foreach (var c in Clients.ToList())
                {
                    try { c.Send(msg); } catch { }
                }
            }
        }

        public static void SendAllSeats(ClientHandler c)
        {
            lock (_lock)
            {
                foreach (var s in Seats)
                    c.Send($"SEAT {s.Id} {s.Status} {s.BookedBy}");
            }
        }

        public static Seat GetSeat(int id)
        {
            return Seats.FirstOrDefault(x => x.Id == id);
        }

        public static void Remove(ClientHandler c)
        {
            lock (_lock) Clients.Remove(c);
        }
    }

    class ClientHandler
    {
        TcpClient client;
        StreamReader reader;
        StreamWriter writer;
        bool running = true;

        public ClientHandler(TcpClient client)
        {
            this.client = client;
            var ns = client.GetStream();
            reader = new StreamReader(ns, Encoding.UTF8);
            writer = new StreamWriter(ns, Encoding.UTF8) { AutoFlush = true };
        }

        public void Send(string msg) => writer.WriteLine(msg);

        public void Run()
        {
            try
            {
                while (running)
                {
                    string line = reader.ReadLine();
                    if (line == null) break;

                    HandleCommand(line);
                }
            }
            catch { }
            finally
            {
                client.Close();
                Program.Remove(this);
                Console.WriteLine("Client disconnected.");
            }
        }

        void HandleCommand(string line)
        {
            Console.WriteLine("Recv: " + line);
            string[] p = line.Split(' ');

            switch (p[0])
            {
                case "LIST":
                    Program.SendAllSeats(this);
                    break;

                case "BOOK":
                    Book(int.Parse(p[1]), p[2]);
                    break;

                case "CANCEL":
                    Cancel(int.Parse(p[1]), p[2]);
                    break;
            }
        }

        void Book(int id, string user)
        {
            var s = Program.GetSeat(id);
            if (s == null) { Send("ERR seat"); return; }

            if (s.IsBooked && s.BookedBy != user)
            {
                Send("ERR booked");
                return;
            }

            s.IsBooked = true;
            s.BookedBy = user;
            Send("OK");
            Program.Broadcast(s);
        }

        void Cancel(int id, string user)
        {
            var s = Program.GetSeat(id);
            if (s == null) { Send("ERR seat"); return; }

            s.IsBooked = false;
            s.BookedBy = "";
            Send("OK");
            Program.Broadcast(s);
        }
    }
}
