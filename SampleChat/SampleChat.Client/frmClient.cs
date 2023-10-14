using ChatLib.Events;
using ChatLib.Handler;
using ChatLib.Models;
using ChatLib.Sockets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleChat.Client
{
    public partial class frmClient : Form
    {
        private ChatClient _client;
        private ClientHandler? _clientHandler;

        public frmClient()
        {
            InitializeComponent();

            _client = new ChatClient(IPAddress.Parse("127.0.0.1"), 8080);

            _client.Connected += Connected;
            //_client.Disconnected += Disconnected;
            //_client.Received += Received;
            //_client.RunningStateChanged += RunningStateChanged;
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            await _client.ConnectAsync(new ChatConnection()
            {
                RoomId = Convert.ToInt32(txtRoomId.Text),
                UserName = txtName.Text
            });
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }

        private void Connected(object? sender, ChatEventArgs e)
        {
            _clientHandler = e.ClientHandler;
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _clientHandler?.Send(new ChatInfo()
                {
                    RoomId = Convert.ToInt32(txtRoomId.Text),
                    UserName = txtName.Text
                });
                txtMessage.Clear();
            }

        }
    }
}
