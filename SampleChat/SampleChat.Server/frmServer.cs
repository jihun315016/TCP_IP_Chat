using ChatLib.Events;
using ChatLib.Handler;
using ChatLib.Models;
using ChatLib.Sockets;
using System.Diagnostics;
using System.Net;

namespace SampleChat.Server
{
    public partial class frmServer : Form
    {
        private ChatServer _server;
        private ClientRoomManager _roomManager;

        public frmServer()
        {
            InitializeComponent();

            _server = new ChatServer(IPAddress.Parse("127.0.0.1"), 8080);
            _server.Connected += Connected;
            _server.Disconnected += Disconnected;
            _server.Received += Received;

            _roomManager = new ClientRoomManager();
        }        

        private void btnStart_Click(object sender, EventArgs e)
        {
            _ = _server.StartAsync();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _server.Stop(); 
        }

        /// <summary>
        /// [Connected �̺�Ʈ �߻� ���]
        /// btnStart_Click
        /// >> _server.StartAsync 
        /// >> clientHandler.HandleClientAsync - �����͸� �о��� �� ù �����̶��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Connected(object? sender, ChatEventArgs e) 
        {
            // State�� Initial���� Connect�� �ٲپ� �����ؾ��ϱ� ������
            // ���ο� �ν��Ͻ� ����
            var chatInfo = new ChatInfo()
            {
                RoomId = e.ChatInfo.RoomId,
                UserName = e.ChatInfo.UserName,
                State = ChatState.Connect
            };

            _roomManager.Add(e.ClientHandler);
            _roomManager.SendToMyRoom(chatInfo);

            AddClientMessageList(chatInfo);
        }

        /// <summary>
        /// [Disconnected �̺�Ʈ �߻� ���]
        /// frmClient���� btnStop�� Ŭ���ϸ�
        /// _server.StartAsync 
        /// >> clientHandler.HandleClientAsync ���� ����
        /// >> Disconnected �̺�Ʈ �߻�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Disconnected(object? sender, ChatEventArgs e)
        {

            var chatInfo = new ChatInfo()
            {
                RoomId = e.ChatInfo.RoomId,
                UserName = e.ChatInfo.UserName,
                State = ChatState.Disconnect
            };

            _roomManager.Remove(e.ClientHandler);
            _roomManager.SendToMyRoom(chatInfo);

            lbxClients.Items.Remove(chatInfo);
            AddClientMessageList(chatInfo);
        }

        private void Received(object? sender, ChatEventArgs e)
        {
            _roomManager.SendToMyRoom(e.ChatInfo);

            AddClientMessageList(e.ChatInfo);
        }

        /// <summary>
        /// ���� ���¿� ���� ListBox�� �޽��� �Է�
        /// </summary>
        /// <param name="chatInfo"></param>
        private void AddClientMessageList(ChatInfo chatInfo)
        {
            string message = chatInfo.State switch
            {
                ChatState.Connect => $"[����] {chatInfo}",
                ChatState.Disconnect => $"[���� ����] {chatInfo}",
                _ => $"{chatInfo.UserName} : {chatInfo.Message}"
            };

            lbxMsg.Items.Add(message);
        }
    }
}