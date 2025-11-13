using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Lab03_Bai06
{
    public partial class Lab3B6 : Form
    {
        List<Client> clients = new List<Client>();
        Server server = null;
        Setting setting = null;
        public Lab3B6()
        {
            InitializeComponent();
        }

        private void Server_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                server = new Server();
                server.Visible = true;
                server.FormClosed += (s, _) => { server = null; };
            }
            else
            {
                server.Focus();
            }
        }

        private void Client_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            clients.Add(client);
            client.Visible = true;
            client.FormClosed += (s, _) => { clients.Remove(client); };
        }

        private void Setting_Click(object sender, EventArgs e)
        {
            if (setting == null)
            {
                setting = new Setting();
                setting.Visible = true;
                setting.FormClosed += (s, _) => { setting = null; };
            }
            else
            {
                setting.Focus();
            }
        }
    }
}
