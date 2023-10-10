using ChatLib;
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
        private readonly ILogger<Worker> _logger;

        private Logging _log4net;
        private IConfiguration _configuration;
        private TcpListener _listener;

        public Worker(ILogger<Worker> logger, Logging log4net, IConfiguration configuration)
        {
            _logger = logger;
            _log4net = log4net;
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
                    // ���⿡ ���񽺰� ������ �� ������ �ڵ带 ��������.
                    StopServer();
                });

                ConnectServer(address, port);
                _log4net.WriteInfo($"���� ����");
                _logger.LogInformation($"���� ����");


                while (true)
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync();
                    _log4net.WriteInfo("������ ����");
                    _logger.LogInformation("������ ����");

                    _ = HandleClient(client);
                }
            }
            catch (Exception ex)
            {
                _log4net.WriteError(ex.Message);
                _logger.LogInformation(ex.Message);
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
            _logger.LogInformation("The service is stopping...");
            _listener.Stop();
            _log4net.WriteInfo($"���� ����");
            Console.WriteLine($"���� ���� {DateTime.Now}");
        }


        public async void Listen()
        {
            while (true)
            {
                TcpClient client = await _listener.AcceptTcpClientAsync();
                _logger.LogInformation("������ ����");

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

                _logger.LogInformation($"���� �о���");


                string message = Encoding.UTF8.GetString(buffer, 0, read);

                var hub = ChatHub.Parse(message);

                string strMessage = $"UserId : {hub.UserId}, RoomId : {hub.RoomId}, UserName : {hub.UserName}, Message : {hub.Message}";

                var messageBuffer = Encoding.UTF8.GetBytes($"[Server] {strMessage}");
                stream.Write(messageBuffer, 0, messageBuffer.Length);
            }
        }
    }
}