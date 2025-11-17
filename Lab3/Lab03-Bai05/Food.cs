using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab03_Bai05
{
    public partial class Food : Form
    {
        public Food(DatabaseHelper.MonAn food)
        {
            InitializeComponent();
            txtFood.Text = food.TenMon;
            txtUser.Text = food.NguoiDung;

            if (!string.IsNullOrEmpty(food.HinhAnh) && File.Exists(food.HinhAnh))
            {
                picImage.Image = Image.FromFile(food.HinhAnh);
            }
            else
            {
                picImage.Image = null;
            }
        }
    }
}
