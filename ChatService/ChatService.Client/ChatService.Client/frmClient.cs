using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatService.Client
{
    public partial class frmClient : Form
    {
        private ChatClient _client;

        public frmClient()
        {
            InitializeComponent();
        }
        private void frmClient_Load(object sender, EventArgs e)
        {
            txtAddress.Text = "127.0.0.1";
            txtPort.Text = "8080";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            _client = new ChatClient(txtAddress.Text, Convert.ToInt32(txtPort.Text));
            _client.StartAsync();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _client.Send(new Data()
                {
                    Name = txtName.Text,
                    Message = txtMessage.Text
                });

                txtMessage.Clear();
            }
        }
    }
}
