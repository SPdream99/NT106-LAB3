using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Lab03_Bai02
{
    public partial class Form : System.Windows.Forms.Form
    {
        Socket socket;   // socket server
        Socket client;   // socket client
        public Form()
        {
            InitializeComponent();
        }

        private void button_Listen_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo socket TCP
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Gán socket lắng nghe tại cổng 8080 trên tất cả IP
                socket.Bind(new IPEndPoint(IPAddress.Any, 8080));
                socket.Listen(5);

                button_Listen.Enabled = false;
                listView_Message.Items.Add("Đang lắng nghe...");

                // Bắt đầu chấp nhận kết nối không đồng bộ
                socket.BeginAccept(AcceptCallback, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                client = socket.EndAccept(ar);
                listView_Message.Items.Add("Kết nối thành công!");

                Thread thread = new Thread(HandleClient);
                thread.IsBackground = true;
                thread.Start();

                socket.BeginAccept(AcceptCallback, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void HandleClient()
        {
            try
            {
                StringBuilder dataBuilder = new StringBuilder();
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int bytesReceive = client.Receive(buffer);
                    if (bytesReceive == 0)
                        break;

                    string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesReceive);
                    dataBuilder.Append(receivedData);

                    // Nếu client gửi xuống \n thì kết thúc 1 dòng
                    if (receivedData.EndsWith(Environment.NewLine))
                    {
                        string data = dataBuilder.ToString();

                        listView_Message.Invoke(new Action(() =>
                        {
                            ListViewItem item = new ListViewItem(data.Trim());
                            listView_Message.Items.Add(item);
                        }));

                        dataBuilder.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
