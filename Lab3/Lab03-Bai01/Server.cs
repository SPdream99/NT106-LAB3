using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Lab03_Bai01
{
    public partial class Server : Form
    {
        private UdpClient udpServer;
        private Thread receiveThread;
        private bool isRunning = false;
        public Server()
        {
            InitializeComponent();
        }

        private void button1_Listen_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(textBox_Port.Text.Trim(), out int port) || port < 1 || port > 65535)
                {
                    MessageBox.Show("Port không hợp lệ!");
                    return;
                }

                udpServer = new UdpClient(port);
                isRunning = true;

                receiveThread = new Thread(ReceiveData);
                receiveThread.IsBackground = true;
                receiveThread.Start();

                listbox_message.Items.Add($"Server đang lắng nghe tại cổng {port}...");
                button1_Listen.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể khởi động Server: " + ex.Message);
            }
        }

        private void ReceiveData()
        {
            IPEndPoint clientEP = new IPEndPoint(IPAddress.Any, 0);

            try
            {
                while (isRunning)
                {
                    byte[] buffer = udpServer.Receive(ref clientEP);
                    string message = Encoding.UTF8.GetString(buffer);

                    string line = $"{clientEP.Address}:{message}";
                    AddMessage(line);
                }
            }
            catch (SocketException)
            {
                // socket bị đóng
            }
            catch (ObjectDisposedException)
            {
                // socket disposed
            }
            catch (Exception ex)
            {
                AddMessage("Lỗi: " + ex.Message);
            }
        }

        private void AddMessage(string msg)
        {
            if (listbox_message.InvokeRequired)
            {
                listbox_message.Invoke((MethodInvoker)(() => AddMessage(msg)));
                return;
            }

            listbox_message.Items.Add(msg);
        }



        // Cho phép MainForm đóng thật khi thoát app
        public void ForceStopServer()
        {
            try
            {
                isRunning = false;                 // dừng vòng while nhận dữ liệu
                udpServer?.Close();                // đóng socket
                udpServer?.Dispose();

                if (receiveThread != null && receiveThread.IsAlive)
                    receiveThread.Join(100);       // đợi thread kết thúc (100ms)
            }
            catch
            {
                // tránh crash khi đóng
            }
        }
    }
}
