using System;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Lab03_Bai05
{
    public partial class Lab305 : Form
    {
        public Lab305()
        {
            InitializeComponent();
            LoadFoodList();
            txtIP.Text = '192.168.1.5';
            txtPort = '8080';
        }
        private void LoadFoodList()
        {
            lstFoodList.Items.Clear();
            try
            {
                using (TcpClient client = new TcpClient("127.0.0.1", 5000))
                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
                using (StreamReader reader = new StreamReader(stream))
                {
                    writer.WriteLine("GET_FOOD_LIST");
                    string response = reader.ReadLine();
                    if (!string.IsNullOrEmpty(response))
                    {
                        string[] foods = response.Split('|');
                        foreach (var food in foods)
                            lstFoodList.Items.Add(food);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách: " + ex.Message);
            }
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFood.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                picImage.Image == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            try
            {
                using (TcpClient client = new TcpClient("127.0.0.1", 5000))
                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
                using (StreamReader reader = new StreamReader(stream))
                {
                    string command = $"ADD_FOOD|{txtFood.Text}|{txtBrowseImage.Text}|{txtName.Text}";
                    writer.WriteLine(command);
                    string result = reader.ReadLine();
                    if (result == "OK")
                    {
                        MessageBox.Show("Thêm món ăn thành công!");
                        txtFood.Clear();
                        txtName.Clear();
                        txtBrowseImage.Clear();
                        picImage.Image = null;
                        LoadFoodList();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi: " + result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi dữ liệu: " + ex.Message);
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn hình ảnh món ăn";
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picImage.Image = Image.FromFile(ofd.FileName);
                txtBrowseImage.Text = ofd.FileName;
            }
        }
        private void btnSelectFood_Click(object sender, EventArgs e)
        {
            try
            {
                using (TcpClient client = new TcpClient("127.0.0.1", 5000))
                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
                using (StreamReader reader = new StreamReader(stream))
                {
                    writer.WriteLine("SELECT_FOOD");
                    string response = reader.ReadLine();
                    if (string.IsNullOrEmpty(response))
                    {
                        MessageBox.Show("Danh sách món ăn trống!");
                        return;
                    }
                    var parts = response.Split('|');
                    var selectedFood = new DatabaseHelper.MonAn
                    {
                        TenMon = parts[0],
                        HinhAnh = parts[1],
                        NguoiDung = parts[2]
                    };

                    Food foodForm = new Food(selectedFood);
                    foodForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy món ăn: " + ex.Message);
            }
        }
    }
}
