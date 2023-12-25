using DBApp.Server.Network;
using Microsoft.Extensions.Configuration;
using System.Net.Sockets;

namespace DBApp.Server
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private DBAppServer _server;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _server = new DBAppServer(configuration, (string msg) => _logger.LogInformation(msg));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TcpClient client;

            try
            {
                _server.StartAsync();
                _logger.LogInformation($"[연결 성공]");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[Error] {ex.Message}{Environment.NewLine}{ex.StackTrace}");
            }

        }
    }
}
