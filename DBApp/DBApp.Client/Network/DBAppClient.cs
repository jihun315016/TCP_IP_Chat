using DBApp.Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace DBApp.Client.Network
{
    public class DBAppClient
    {
        private Data _data;
        NetworkStream _stream;

        public DBAppClient(Data data)
        {
            _data = data;
        }

        public async Task StartAsync()
        {
            try
            {
                TcpClient client = new TcpClient();
                await client.ConnectAsync(IPAddress.Parse(_data.Address), Convert.ToInt32(_data.Port));
                _stream = client.GetStream();
                Send(_data);
            }
            catch (Exception ex)
            {

            }
        }

        public void Send(Data data)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize<Data>(data));
                byte[] bufferSize = BitConverter.GetBytes(buffer.Length);

                _stream.Write(bufferSize);
                _stream.Write(buffer);
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
