using ChatLib.Events;
using ChatLib.Managers;
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

            // 원래 ChatServer 객체의 clientHandler 이벤트를 정의해야 하는데
            // clientHandler가 StartAsync 메서드의 지역 변수로 등록되어 있어서
            // _server를 통해 한 다리 건너서 이벤트 전달
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

        public void Connected(object? sender, ChatEventArgs e) 
        {
            // State를 Initial에서 Connect로 바꾸어 전달해야하기 때문에
            // 새로운 인스턴스 생성
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
        /// 연결 상태에 따라 ListBox에 메시지 입력
        /// </summary>
        /// <param name="chatInfo"></param>
        private void AddClientMessageList(ChatInfo chatInfo)
        {
            string message = chatInfo.State switch
            {
                ChatState.Connect => $"[접속] {chatInfo}",
                ChatState.Disconnect => $"[접속 종료] {chatInfo}",
                _ => $"{chatInfo.UserName} : {chatInfo.Message}"
            };

            lbxMsg.Items.Add(message);
        }
    }
}