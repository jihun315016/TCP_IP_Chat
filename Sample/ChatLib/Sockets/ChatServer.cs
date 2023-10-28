using ChatLib.Events;
using ChatLib.Handler;
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
    public class ChatServer : ChatBase
    {
        private readonly TcpListener _listener;

        public override event EventHandler<ChatEventArgs>? Connected;
        public override event EventHandler<ChatEventArgs>? Disconnected;
        public override event EventHandler<ChatEventArgs>? Received;

        public ChatServer(IPAddress iPAddress, int port) : base(iPAddress, port)
        {
            _listener = new TcpListener(iPAddress, port);
        }

        public async Task StartAsync()
        {
            TcpClient client;
            ClientHandler clientHandler;

            if (IsRunning) return;

            try
            {
                _listener.Start();
                IsRunning = true;
                Debug.Print("[서버 시작]");

                while (true)
                {
                    client = await _listener.AcceptTcpClientAsync();
                    Debug.Print($"[클라이언트 연결] {client.Client.Handle}");

                    // ClientHandler와 ChatServer의 이벤트 동작 동기화
                    clientHandler = new ClientHandler(client);
                    clientHandler.Connected += Connected;
                    clientHandler.Disconnected += Disconnected;
                    clientHandler.Received += Received;

                    _ = clientHandler.HandleClientAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"[서버 시작 중 오류 발생] {ex.Message}");
                IsRunning = false;
            }
        }

        public void Stop()
        {
            IsRunning = false;
            _listener.Stop();
            Debug.Print("[서버 중지]");
        }
    }
}
