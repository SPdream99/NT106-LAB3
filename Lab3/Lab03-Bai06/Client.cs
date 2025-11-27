using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Lab03_Bai06
{
    public partial class Client : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        private Thread listenThread;
        private string username;

        public Client()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Vui lòng nhập tên người dùng.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.username = textBox1.Text;
            try
            {
                client = new TcpClient();
                client.Connect(GlobalSettings.ServerAddress, int.Parse(GlobalSettings.Port));

                stream = client.GetStream();
                reader = new StreamReader(stream, Encoding.Unicode);
                writer = new StreamWriter(stream, Encoding.Unicode) { AutoFlush = true };

                writer.WriteLine($"CONNECT|{this.username}");

                listenThread = new Thread(new ThreadStart(ListenForMessages));
                listenThread.IsBackground = true;
                listenThread.Start();

                button1.Enabled = false;
                textBox1.Enabled = false;
                this.Text = $"Chat Client - {this.username}";
                LogMessage("Connected to server.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListenForMessages()
        {
            try
            {
                string message;
                while ((message = reader.ReadLine()) != null)
                {
                    HandleServerMessage(message);
                }
            }
            catch (Exception ex)
            {
                LogMessage("Lost connection to server.");
                LogMessage(ex.Message);
            }
        }

        private void HandleServerMessage(string message)
        {
            string[] parts = message.Split(new char[] { '|' }, 4);
            string command = parts[0];
            switch (command)
            {
                case "MSG":
                case "﻿MSG":
                    LogMessage($"{parts[1]}: {parts[2]}");
                    break;

                case "SYSTEM":
                case "﻿SYSTEM":
                    LogMessage($"[{parts[1]}]");
                    break;

                case "﻿USER_LIST":
                case "USER_LIST":
                    UpdateUserList(parts[1].Split(','));
                    break;

                case "SHUTDOWN":
                case "﻿SHUTDOWN":
                    listenThread.Abort();
                    writer.Close();
                    reader.Close();
                    client.Close();
                    textBox1.Text = null;
                    button1.Enabled = true;
                    textBox1.Enabled = true;
                    UpdateUserList(new string[] { });
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (writer == null || string.IsNullOrEmpty(textBox2.Text)) return;

            string message = textBox2.Text;
            string selectedUser = Users.SelectedItem as string;

            writer.WriteLine($"MSG|{message}");

            textBox2.Clear();
        }

        private void LogMessage(string message)
        {
            try
            {
                if (richTextBox1.InvokeRequired)
                {
                    richTextBox1.Invoke(new MethodInvoker(delegate { LogMessage(message); }));
                }
                else
                {
                    richTextBox1.AppendText($"{message}\n");
                    richTextBox1.ScrollToCaret();
                }
            }
            catch (Exception)
            {

            }
        }

        private void UpdateUserList(string[] users)
        {
            if (Users.InvokeRequired)
            {
                Users.Invoke(new MethodInvoker(delegate { UpdateUserList(users); }));
            }
            else
            {
                Users.Items.Clear();
                foreach (string user in users)
                {
                    Users.Items.Add(user);
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            listenThread?.Abort();
            writer?.Close();
            reader?.Close();
            client?.Close();
        }
    }
}
