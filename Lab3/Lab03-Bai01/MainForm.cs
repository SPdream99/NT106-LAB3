using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03_Bai01
{
    public partial class MainForm: Form
    {
        private Server serverForm;
        public MainForm()
        {
            InitializeComponent();
        }


        //nhấn mở server
        private void Button_OpenServer_Click(object sender, EventArgs e)
        {
            // Nếu chưa có hoặc đã bị đóng, tạo mới
            if (serverForm == null || serverForm.IsDisposed)
            {
                serverForm = new Server();
                serverForm.StartPosition = FormStartPosition.Manual;

                // đặt bên trái màn hình (tuỳ chỉnh)
                var wa = Screen.PrimaryScreen.WorkingArea;
                serverForm.Location = new Point(wa.Left + 50, wa.Top + 50);

                serverForm.Show();
            }
            else
            {
                // Nếu đang ẩn, Show lại
                serverForm.Show();
                serverForm.BringToFront();
            }
        }

        //nhấn mở client
        private void Button_Client_Click(object sender, EventArgs e)
        {
            // Mỗi lần bấm -> mở 1 Client mới (có thể nhiều client)
            var clientForm = new Client();
            clientForm.StartPosition = FormStartPosition.Manual;

            // đặt lệch sang phải để nhìn song song
            var wa = Screen.PrimaryScreen.WorkingArea;
            clientForm.Location = new Point(wa.Width / 2 + 50, wa.Top + 50);

            clientForm.Show();
            clientForm.BringToFront();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try { serverForm?.ForceStopServer(); } catch { }
            base.OnFormClosing(e);
        }
    }
}
