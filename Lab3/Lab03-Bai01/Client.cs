using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Lab03_Bai01
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void Button_Send_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IPAddress.TryParse(textbox_host.Text.Trim(), out var ip))
                {
                    MessageBox.Show("IP không hợp lệ!");
                    return;
                }
                if (!int.TryParse(textbox_port.Text.Trim(), out int port) || port < 1 || port > 65535)
                {
                    MessageBox.Show("Port không hợp lệ!");
                    return;
                }

                string message = Messagebox.Text;
                if (string.IsNullOrWhiteSpace(message))
                {
                    MessageBox.Show("Vui lòng nhập nội dung cần gửi!");
                    return;
                }

                byte[] data = Encoding.UTF8.GetBytes(message);
                using (UdpClient udpClient = new UdpClient())
                {
                    udpClient.Send(data, data.Length, new IPEndPoint(ip, port));
                }

                MessageBox.Show("Đã gửi thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi UDP: " + ex.Message);
            }
        }
    }
}
