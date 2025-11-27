using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Lab03_Bai06
{
    public partial class Server : Form
    {
        private Dictionary<string, TcpClient> clients = new Dictionary<string, TcpClient>();
        private TcpListener listener;
        private Thread listenerThread;

        public Server()
        {
            InitializeComponent();
        }

        private void Server_Load(object sender, EventArgs e)
        {
            
        }

        private void Listen_Click(object sender, EventArgs e)
        {
            listenerThread = new Thread(new ThreadStart(StartListening));
            listenerThread.IsBackground = true;
            listenerThread.Start();

            Listen.Enabled = false;
            LogMessage("Server started. Waiting for connections...");
        }

        private void StartListening()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 8080);
                listener.Start();

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                    clientThread.IsBackground = true;
                    clientThread.Start(client);
                }
            }
            catch (SocketException ex)
            {
                LogMessage($"SocketException: {ex.Message}");
            }
        }

        private void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string username = null;

            try
            {
                string connectMessage = reader.ReadLine();
                username = connectMessage.Split('|')[1];

                lock (clients)
                {
                    clients.Add(username, client);
                }
                LogMessage($"{username} connected from {((IPEndPoint)client.Client.RemoteEndPoint).Address}");
                UpdateClientList();

                BroadcastMessage($"SYSTEM|{username} joined the chat.", username);

                string message;
                while ((message = reader.ReadLine()) != null)
                {
                    ParseAndRelayMessage(username, message);
                }
            }
            catch (Exception)
            {
                if (username != null)
                {
                    lock (clients)
                    {
                        clients.Remove(username);
                    }
                    LogMessage($"{username} disconnected.");
                    UpdateClientList();
                    BroadcastMessage($"SYSTEM|{username} left the chat.", null);
                }
            }
            finally
            {
                client.Close();
            }
        }

        private void ParseAndRelayMessage(string fromUser, string message)
        {
            string[] parts = message.Split(new char[] { '|' }, 4);
            string command = parts[0];

            switch (command)
            {
                case "PUBLIC_MSG":
                    string publicMsg = $"MSG|{fromUser}|{parts[1]}";
                    BroadcastMessage(publicMsg, fromUser);
                    LogMessage($"Public msg from {fromUser}: {parts[1]}");
                    break;
            }
        }
        
        private void BroadcastMessage(string message, string excludeUser)
        {
            lock (clients)
            {
                foreach (var pair in clients)
                {
                    if (pair.Key != excludeUser)
                    {
                        try
                        {
                            StreamWriter writer = new StreamWriter(pair.Value.GetStream(), Encoding.UTF8) { AutoFlush = true };
                            writer.WriteLine(message);
                        }
                        catch {}
                    }
                }
            }
        }

        private void UpdateClientList()
        {
            string userList;
            lock (clients)
            {
                userList = string.Join(",", clients.Keys);
            }

            BroadcastMessage($"USER_LIST|{userList}", null);
        }

        private void LogMessage(string message)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new MethodInvoker(delegate { LogMessage(message); }));
            }
            else
            {
                richTextBox1.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\n");
                richTextBox1.ScrollToCaret();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            listener?.Stop();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            listener?.Stop();
        }
    }
}
