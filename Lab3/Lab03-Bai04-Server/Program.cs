using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Lab03_Bai04_Server
{
    class Seat
    {
        public int Id;
        public bool Booked;
        public string By;
    }

    class Program
    {
        static List<Seat> seats = new List<Seat>();
        static object lockSeats = new object();
        static List<StreamWriter> clients = new List<StreamWriter>();
        static object lockClients = new object();

        static void Main(string[] args)
        {
            Console.Title = "SERVER - Lab03 Bai04";
            // tạo danh sách ghế mẫu
            for (int i = 1; i <= 10; i++)
                seats.Add(new Seat { Id = i, Booked = false, By = "" });

            int port = 8080;
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Console.WriteLine($"=== SERVER đang lắng nghe trên PORT {port} ===");

            try
            {
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient(); // blocking — server sẽ không exit
                    Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint}");
                    ThreadPool.QueueUserWorkItem(HandleClient, client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Listener stopped: " + ex.Message);
            }
            finally
            {
                listener.Stop();
            }
        }

        static void HandleClient(object obj)
        {
            TcpClient tcp = (TcpClient)obj;
            using (NetworkStream ns = tcp.GetStream())
            using (StreamReader reader = new StreamReader(ns, Encoding.UTF8))
            using (StreamWriter writer = new StreamWriter(ns, Encoding.UTF8) { AutoFlush = true })
            {
                lock (lockClients) clients.Add(writer);
                SendSeatList(writer);

                try
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine($"[Client {tcp.Client.RemoteEndPoint}] {line}");
                        var parts = line.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 0) continue;
                        var cmd = parts[0].ToUpper();

                        if (cmd == "LIST")
                        {
                            SendSeatList(writer);
                        }
                        else if (cmd == "BOOK" && parts.Length >= 3)
                        {
                            if (!int.TryParse(parts[1], out int id)) { writer.WriteLine("ERR InvalidId"); continue; }
                            string name = parts[2];
                            lock (lockSeats)
                            {
                                var seat = seats.Find(s => s.Id == id);
                                if (seat == null) writer.WriteLine("ERR NotFound");
                                else if (seat.Booked) writer.WriteLine($"ERR Seat {id} already booked by {seat.By}");
                                else
                                {
                                    seat.Booked = true; seat.By = name;
                                    writer.WriteLine($"OK Booked {id}");
                                    Broadcast($"UPDATE {seat.Id} Booked {seat.By}");
                                }
                            }
                        }
                        else if (cmd == "CANCEL" && parts.Length >= 3)
                        {
                            if (!int.TryParse(parts[1], out int id)) { writer.WriteLine("ERR InvalidId"); continue; }
                            string name = parts[2];
                            lock (lockSeats)
                            {
                                var seat = seats.Find(s => s.Id == id);
                                if (seat == null) writer.WriteLine("ERR NotFound");
                                else if (!seat.Booked) writer.WriteLine($"ERR Seat {id} is not booked");
                                else if (seat.By != name) writer.WriteLine($"ERR Seat {id} booked by {seat.By}");
                                else
                                {
                                    seat.Booked = false; seat.By = "";
                                    writer.WriteLine($"OK Cancelled {id}");
                                    Broadcast($"UPDATE {seat.Id} Free ");
                                }
                            }
                        }
                        else if (cmd == "QUIT")
                        {
                            break;
                        }
                        else
                        {
                            writer.WriteLine("ERR UnknownCommand");
                        }
                    }
                }
                catch (IOException) { /* client disconnected abruptly */ }
                catch (Exception ex) { Console.WriteLine("Client handler error: " + ex.Message); }
                finally
                {
                    lock (lockClients) clients.Remove(writer);
                    try { tcp.Close(); } catch { }
                    Console.WriteLine("Client disconnected.");
                }
            }
        }

        static void SendSeatList(StreamWriter writer)
        {
            lock (lockSeats)
            {
                foreach (var s in seats)
                {
                    writer.WriteLine($"SEAT {s.Id} {(s.Booked ? "Booked" : "Free")} {s.By}");
                }
            }
        }

        static void Broadcast(string msg)
        {
            Console.WriteLine("Broadcast: " + msg);
            lock (lockClients)
            {
                var remove = new List<StreamWriter>();
                foreach (var w in clients)
                {
                    try { w.WriteLine(msg); }
                    catch { remove.Add(w); }
                }
                foreach (var r in remove) clients.Remove(r);
            }
        }
    }
}
