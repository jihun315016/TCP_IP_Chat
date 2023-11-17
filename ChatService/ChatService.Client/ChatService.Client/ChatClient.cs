using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Client
{
    public class ChatClient
    {
        private readonly string _address;
        private readonly int _port;

        public ChatClient(string address, int port)
        {
            _address = address;
            _port = port;
        }


        public async Task StartAsync()
        {
            try
            {
                TcpClient client = new TcpClient();
                await client.ConnectAsync(IPAddress.Parse(_address), _port);
                NetworkStream stream = client.GetStream();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
