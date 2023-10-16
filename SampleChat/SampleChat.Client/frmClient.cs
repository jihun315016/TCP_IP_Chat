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
            _client.Disconnected += Disconnected;
            _client.Received += Received;
            _client.RunningStateChanged += RunningStateChanged;
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
            _client.Close();
        }

        private void Connected(object? sender, ChatEventArgs e)
        {
            _clientHandler = e.ClientHandler;
        }

        /// <summary>
        /// Disconnected 이벤트 발생 경로

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Disconnected(object? sender, ChatEventArgs e)
        {
            _clientHandler = null;
            lbxMsg.Items.Add("서버의 연결이 끊어졌습니다.");
        }

        private void Received(object? sender, ChatLib.Events.ChatEventArgs e)
        {
            string message = e.ChatInfo.State switch
            {
                ChatState.Connect => $"{e.ChatInfo.UserName}님이 접속하였습니다.",
                ChatState.Disconnect => $"{e.ChatInfo.UserName}님이 종료하였습니다.",
                _ => $"{e.ChatInfo.UserName} : {e.ChatInfo.Message}"
            };

            lbxMsg.Items.Add(message);
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _clientHandler?.Send(new ChatInfo()
                {
                    RoomId = Convert.ToInt32(txtRoomId.Text),
                    UserName = txtName.Text,
                    Message = txtMessage.Text
                });
                txtMessage.Clear();
            }
        }

        private void RunningStateChanged(bool isRunning)
        {
            btnConnect.Enabled = !isRunning;
            btnStop.Enabled = isRunning;
        }
    }
}
