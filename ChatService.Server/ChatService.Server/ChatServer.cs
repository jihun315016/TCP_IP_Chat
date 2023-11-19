using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders.Physical;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ChatService.Server
{

    public class ChatServer
    {
        public Action<string> ShowMessage;

        private TcpListener _listener;
        private readonly string _address;
        private readonly int _port;

        public string ConnectionInfo
        {
            get { return $"{_address}:{_port}"; }
        }


        public ChatServer(IConfiguration configuration, Action<string> showMessage)
        {
            _address = Utility.GetAppSettings<string>(configuration, "ServerInfo", "Address");
            _port = Utility.GetAppSettings<int>(configuration, "ServerInfo", "Port");

            if (_address == default)
            {
                _address = "127.0.0.1";
            }

            if (_port == default)
            {
                _port = 8080;
            }

            _listener = new TcpListener(IPAddress.Parse(_address), _port);

            ShowMessage += showMessage;
        }
        

        public async Task StartAsync()
        {
            TcpClient client;
            byte[] bufferSize = new byte[4];
            byte[] buffer;
            int read, size;
            string message;
            Data data;

            try
            {
                _listener.Start();

                while (true)
                {
                    client = await _listener.AcceptTcpClientAsync();
                    ShowMessage("[클라이언트에서 연결되었음]");

                    
                    _ = HandleClientAsync(client);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("!!!");
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            byte[] bufferSize = new byte[4];
            byte[] buffer;
            int read, size;
            string message;
            Data data;

            try
            {
                // 반한된 네트워크 스트림을 통해서 데이터를 주고 받을 수 있음
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    read = await stream.ReadAsync(bufferSize, 0, bufferSize.Length);
                    if (read == 0) break;

                    size = BitConverter.ToInt32(bufferSize, 0);
                    buffer = new byte[size];

                    read = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (read == 0) break;

                    message = Encoding.UTF8.GetString(buffer);

                    ShowMessage("???");

                    //message = Encoding.UTF8.GetString(buffer);
                    //data = JsonSerializer.Deserialize<Data>(message);

                    //ShowMessage($"[{data.Name}] {data.Message}");
                }
            }
            
            catch (Exception ex)
            {
                ShowMessage("!!!");
            }
        }

        public void Stop()
        {
        }
    }
}
