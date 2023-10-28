using ChatLib.Events;
using ChatLib.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatLib.Handler
{
    public class ClientHandler : ChatEventBase
    {
        public override event EventHandler<ChatEventArgs>? Connected;
        public override event EventHandler<ChatEventArgs>? Disconnected;
        public override event EventHandler<ChatEventArgs>? Received;

        private readonly TcpClient _client;
        private readonly NetworkStream _stream;

        public ChatInfo? InitialData { get; private set; }

        public ClientHandler(TcpClient client)
        {
            _client = client;
            _stream = client.GetStream();
        }

        public async Task HandleClientAsync()
        {
            byte[] sizeBuffer = new byte[4];
            byte[] buffer;
            int read, size;
            string message;
            ChatInfo chatInfo;

            try
            {
                while (true) 
                {
                    read = await _stream.ReadAsync(sizeBuffer, 0, sizeBuffer.Length);
                    if (read == 0) break;

                    size = BitConverter.ToInt32(sizeBuffer, 0);
                    buffer = new byte[size];

                    read = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    if (read == 0) break;

                    message = Encoding.UTF8.GetString(buffer);
                    chatInfo = JsonSerializer.Deserialize<ChatInfo>(message);

                    if (chatInfo.State == ChatState.Initial)
                    {
                        InitialData = chatInfo;
                        Debug.Print("[클라이언트 연결 이벤트 발생]");
                        Connected?.Invoke(this, new ChatEventArgs(this, chatInfo));
                    }
                    else
                    {
                        Debug.Print("[클라이언트 데이터 수신 이벤트 발생]");
                        Received?.Invoke(this, new ChatEventArgs(this, chatInfo));
                    }
                }
            }
            catch (Exception ex) 
            {
                Debug.Print($"[데이터 통신 중 오류 발생] {ex.Message}");
            }
            finally
            {
                _client.Close();
                Disconnected?.Invoke(this, new ChatEventArgs(this, InitialData));
            }
        }

        public void Send(ChatInfo chatInfo)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize<ChatInfo>(chatInfo));
                byte[] sizeBuffer = BitConverter.GetBytes(buffer.Length);

                _stream.Write(sizeBuffer);
                _stream.Write(buffer);
            }
            catch (Exception ex)
            {
                Debug.Print($"[데이터 전송 중 오류 발생] {ex.Message}");
            }
        }
    }
}
