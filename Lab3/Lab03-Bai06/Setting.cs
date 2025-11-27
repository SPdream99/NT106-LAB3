using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03_Bai06
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
            textBox1.Text = GlobalSettings.ServerAddress;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox1.Text, @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\." +
                                @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\." +
                                @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\." +
                                @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")) {
                GlobalSettings.ServerAddress = textBox1.Text;
                button1.DialogResult = DialogResult.OK;
                MessageBox.Show("Saved");
            }
            else
            {
                button1.DialogResult = DialogResult.Cancel;
                MessageBox.Show("Invalid IP Address", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
