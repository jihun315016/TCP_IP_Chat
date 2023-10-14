using ChatLib.Events;
using ChatLib.Models;
using ChatLib.Sockets;
using System.Diagnostics;
using System.Net;

namespace SampleChat.Server
{
    public partial class frmServer : Form
    {
        private ChatServer _server;

        public frmServer()
        {
            InitializeComponent();

            _server = new ChatServer(IPAddress.Parse("127.0.0.1"), 8080);
            _server.Connected += Connected;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _ = _server.StartAsync();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _server.Stop(); 
        }

        public void Connected(object? sender, ChatEventArgs e) 
        {
            var chatInfo = new ChatInfo()
            {
                UserName = e.ChatInfo.UserName,
                State = ChatState.Connect
            };

            lbxMsg.Items.Add(e.ChatInfo);
            AddClientMessageList(e.ChatInfo);
        }

        private void AddClientMessageList(ChatInfo chatInfo)
        {
            string message = chatInfo.State switch
            {
                ChatState.Initial => $"[立加] {chatInfo}",
                ChatState.Disconnect => $"[立加 辆丰] {chatInfo}",
                _ => $"{chatInfo.UserName} : {chatInfo.Message}"
            };

            lbxMsg.Items.Add(message);
        }
    }
}