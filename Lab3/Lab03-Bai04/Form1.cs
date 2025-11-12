using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03_Bai04
{
    public partial class Form1 : Form
    {
        // Network fields
        TcpClient client;
        NetworkStream ns;
        StreamReader reader;
        StreamWriter writer;
        Thread readThread;
        bool connected = false;

        public Form1()
        {
            InitializeComponent();
            txtServerIP.Text = "127.0.0.1";
            txtPort.Text = "8080";
            btnDisconnect.Enabled = false;
            btnBook.Enabled = false;
            btnCancel.Enabled = false;
            // đảm bảo ListView có 3 columns (nếu chưa tạo Designer)
            if (lvSeats.Columns.Count == 0)
            {
                lvSeats.Columns.Add("ID", 60);
                lvSeats.Columns.Add("Status", 100);
                lvSeats.Columns.Add("BookedBy", 140);
            }
        }

        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lvSeats_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (connected) return;
            string ip = txtServerIP.Text.Trim();
            if (!int.TryParse(txtPort.Text.Trim(), out int port))
            {
                MessageBox.Show("Port không hợp lệ");
                return;
            }
            try
            {
                client = new TcpClient();
                client.Connect(ip, port);
                ns = client.GetStream();
                reader = new StreamReader(ns, Encoding.UTF8);
                writer = new StreamWriter(ns, Encoding.UTF8) { AutoFlush = true };
                connected = true;

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                btnBook.Enabled = true;
                btnCancel.Enabled = true;
                tssStatus.Text = "Connected";

                lbLog.Items.Add("Connected to server " + ip + ":" + port);

                // start reader thread
                readThread = new Thread(ReadLoop) { IsBackground = true };
                readThread.Start();

                // request initial list
                writer.WriteLine("LIST");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối: " + ex.Message);
            }
        }


        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            DisconnectClient();
        }

        private void DisconnectClient()
        {
            try { if (writer != null && connected) writer.WriteLine("QUIT"); } catch { }
            try { reader?.Close(); } catch { }
            try { writer?.Close(); } catch { }
            try { ns?.Close(); } catch { }
            try { client?.Close(); } catch { }

            connected = false;
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            btnBook.Enabled = false;
            btnCancel.Enabled = false;
            tssStatus.Text = "Disconnected";
            lbLog.Items.Add("Disconnected from server");
        }


        private void btnBook_Click(object sender, EventArgs e)
        {
            if (!connected) { MessageBox.Show("Chưa kết nối tới server"); return; }
            if (lvSeats.SelectedItems.Count == 0) { MessageBox.Show("Chọn ghế trước"); return; }
            if (string.IsNullOrWhiteSpace(txtUsername.Text)) { MessageBox.Show("Nhập username"); return; }

            string id = lvSeats.SelectedItems[0].Text;
            string name = txtUsername.Text.Trim();
            try
            {
                writer.WriteLine($"BOOK {id} {name}");
                lbLog.Items.Add($"Sent: BOOK {id} {name}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gửi lệnh thất bại: " + ex.Message);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!connected) { MessageBox.Show("Chưa kết nối tới server"); return; }
            if (lvSeats.SelectedItems.Count == 0) { MessageBox.Show("Chọn ghế trước"); return; }
            if (string.IsNullOrWhiteSpace(txtUsername.Text)) { MessageBox.Show("Nhập username"); return; }

            string id = lvSeats.SelectedItems[0].Text;
            string name = txtUsername.Text.Trim();
            try
            {
                writer.WriteLine($"CANCEL {id} {name}");
                lbLog.Items.Add($"Sent: CANCEL {id} {name}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gửi lệnh thất bại: " + ex.Message);
            }
        }


        private void ReadLoop()
        {
            try
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    HandleServerMessage(line);
                }
            }
            catch
            {
                // connection lost
            }
            finally
            {
                // đảm bảo UI cập nhật
                this.BeginInvoke((Action)(() => DisconnectClient()));
            }
        }



        private void HandleServerMessage(string msg)
        {
            this.BeginInvoke((Action)(() =>
            {
                if (string.IsNullOrWhiteSpace(msg)) return;

                if (msg.StartsWith("SEAT "))
                {
                    ParseSeatLine(msg);
                }
                else if (msg.StartsWith("UPDATE "))
                {
                    var parts = msg.Split(new[] { ' ' }, 4, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 3 && int.TryParse(parts[1], out int id))
                    {
                        string status = parts[2];
                        string by = parts.Length >= 4 ? parts[3] : "";
                        UpdateSeatInList(id, status, by);
                        lbLog.Items.Add(msg);
                    }
                }
                else if (msg.StartsWith("OK ") || msg.StartsWith("ERR "))
                {
                    lbLog.Items.Add(msg);
                }
                else
                {
                    lbLog.Items.Add(msg);
                }

                if (lbLog.Items.Count > 0) lbLog.TopIndex = lbLog.Items.Count - 1;
            }));
        }

        private void ParseSeatLine(string line)
        {
            // SEAT <id> <status> <by?>
            var parts = line.Split(new[] { ' ' }, 4, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 3 && int.TryParse(parts[1], out int id))
            {
                string status = parts[2];
                string by = parts.Length >= 4 ? parts[3] : "";
                UpdateSeatInList(id, status, by);
            }
        }

        private void UpdateSeatInList(int id, string status, string by)
        {
            ListViewItem found = null;
            foreach (ListViewItem it in lvSeats.Items)
            {
                if (it.Text == id.ToString()) { found = it; break; }
            }

            if (found == null)
            {
                var item = new ListViewItem(id.ToString());
                item.SubItems.Add(status);
                item.SubItems.Add(by);
                lvSeats.Items.Add(item);
            }
            else
            {
                if (found.SubItems.Count < 3)
                {
                    while (found.SubItems.Count < 3) found.SubItems.Add(string.Empty);
                }
                found.SubItems[1].Text = status;
                found.SubItems[2].Text = by;
            }

            // màu sắc gợi ý
            try
            {
                var it = found ?? lvSeats.Items[lvSeats.Items.Count - 1];
                if (it.SubItems[1].Text == "Booked") it.BackColor = System.Drawing.Color.LightSalmon;
                else it.BackColor = System.Drawing.Color.LightGreen;
            }
            catch { }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { DisconnectClient(); } catch { }
        }

        private void lbLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ...
        }

    }
}
