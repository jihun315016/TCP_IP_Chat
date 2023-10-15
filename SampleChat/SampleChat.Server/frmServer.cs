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
        /// [Connected 이벤트 발생 경로]
        /// btnStart_Click
        /// >> _server.StartAsync 
        /// >> clientHandler.HandleClientAsync - 데이터를 읽었을 때 첫 연결이라면
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// [Disconnected 이벤트 발생 경로]
        /// frmClient에서 btnStop을 클릭하면
        /// _server.StartAsync 
        /// >> clientHandler.HandleClientAsync 루프 종료
        /// >> Disconnected 이벤트 발생
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