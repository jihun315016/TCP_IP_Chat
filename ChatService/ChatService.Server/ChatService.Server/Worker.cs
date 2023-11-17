using ChatService.Server.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Sockets;

namespace ChatService.Server
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private TcpListener _listener;
        private IConfiguration _configuration;
        private Connector _connector;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;

            string address;
            int port;

            _connector = new Connector(configuration, (string msg) => _logger.LogInformation(msg)); 
            _logger.LogInformation($"[연결 준비] {_connector.ConnectionInfo}");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}

            TcpClient client;

            try
            {
                _connector.StartAsync();
                _logger.LogInformation($"[연결 성공] {_connector.ConnectionInfo}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[Error] {ex.Message}{Environment.NewLine}{ex.StackTrace}");
            }
        }        
    }
}