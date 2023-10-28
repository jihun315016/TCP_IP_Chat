using ChatLib.Events;
using ChatLib.Handler;
using ChatLib.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib.Sockets
{
    public class ChatClient : ChatBase
    {
        private TcpClient? _client;

        public override event EventHandler<ChatEventArgs>? Connected;
        public override event EventHandler<ChatEventArgs>? Disconnected;
        public override event EventHandler<ChatEventArgs>? Received;

        public ChatClient(IPAddress iPAddress, int port) : base(iPAddress, port) { }

        public async Task ConnectAsync(ChatConnection chatConnection)
        {
            ChatInfo chatInfo;
            ClientHandler clientHandler;

            if (IsRunning) return;

            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(IPAddress, Port);
                IsRunning = true;

                chatInfo = new ChatInfo()
                {
                    UserName = chatConnection.UserName,
                    RoomId = chatConnection.RoomId,
                    State = ChatState.Initial
                };

                clientHandler = new ClientHandler(_client);

                // 연결 성공 시 Connected 이벤트 발생
                // frmClient.Connected => _clientHandler = e.ClientHandler;
                Connected?.Invoke(this, new ChatEventArgs(clientHandler, chatInfo));

                clientHandler.Disconnected += ClientHandler_Disconnected;

                // Received 이벤트는 ChatClient와 ClientHandler 동기화
                clientHandler.Received += Received;

                _ = clientHandler.HandleClientAsync();
                clientHandler?.Send(chatInfo);
            }
            catch (Exception ex)
            {
                DisposeClient();
                Debug.Print($"[서버 연결 시도 중 오류 발생] {ex.Message}");
            }
        }

        private void DisposeClient()
        {
            _client?.Dispose();
            IsRunning = false;
        }

        private void ClientHandler_Disconnected(object? sender, ChatEventArgs e)
        {
            DisposeClient();
            Disconnected?.Invoke(sender, e);
        }

        public void Close()
        {
            DisposeClient();
        }
    }
}
