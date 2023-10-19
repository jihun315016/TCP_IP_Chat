using ChatLib;
using ChatServer.Loggings;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;


namespace ChatServer
{
    public class Worker : BackgroundService
    {
        private readonly UnifiedLogger _logger;
        private IConfiguration _configuration;
        private TcpListener _listener;

        public Worker(ILogger<Worker> logger, Logging log4net, IConfiguration configuration)
        {
            _logger = new UnifiedLogger(logger, log4net);
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IConfigurationSection serverInfo;
            string address;
            int port;

            try
            {
                serverInfo = _configuration.GetSection("ServerInfo");
                address = serverInfo.GetValue<string>("Address");
                port = serverInfo.GetValue<int>("Port");

                stoppingToken.Register(() =>
                {
                    // 여기에 서비스가 중지될 때 실행할 코드를 넣으세요.
                    StopServer();
                });

                ConnectServer(address, port);
                _logger.WriteInfo("연결 성공");


                while (true)
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync();
                    _logger.WriteInfo("누군가 들어옴");

                    _ = HandleClient(client);
                }
            }
            catch (Exception ex)
            {
                _logger.WriteError(ex.Message);
            }
            finally
            {
                serverInfo = null;
            }
        }

        public void ConnectServer(string addresss, int port)
        {
            _listener = new TcpListener(IPAddress.Parse(addresss), port);
            _listener.Start();
        }

        public void StopServer()
        {
            _logger.WriteInfo("서비스 종료 중...");
            _listener.Stop();
            _logger.WriteInfo("연결 종료");
        }


        public async void Listen()
        {
            while (true)
            {
                TcpClient client = await _listener.AcceptTcpClientAsync();
                _logger.WriteInfo("누군가 들어옴");

                _ = HandleClient(client);
            }
        }

        private async Task HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            byte[] sizeBuffer = new byte[4];
            int read;

            while (true)
            {
                read = await stream.ReadAsync(sizeBuffer, 0, sizeBuffer.Length);
                if (read == 0)
                    break;

                int size = BitConverter.ToInt32(sizeBuffer, 0);

                byte[] buffer = new byte[size];
                read = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (read == 0)
                    break;

                _logger.WriteInfo("무언가 읽었음");


                string message = Encoding.UTF8.GetString(buffer, 0, read);

                var hub = ChatHub.Parse(message);

                string strMessage = $"UserId : {hub.UserId}, RoomId : {hub.RoomId}, UserName : {hub.UserName}, Message : {hub.Message}";

                var messageBuffer = Encoding.UTF8.GetBytes($"[Server] {strMessage}");
                stream.Write(messageBuffer, 0, messageBuffer.Length);
            }
        }
    }
}