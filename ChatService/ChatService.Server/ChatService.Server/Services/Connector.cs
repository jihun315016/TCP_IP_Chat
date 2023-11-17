using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders.Physical;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ChatService.Server.Services
{

    public class Connector
    {
        public Action<string> ShowMessage;

        public string Address { get; }

        public int Port { get; }

        public string ConnectionInfo 
        {
            get  { return $"{Address}:{Port}"; }
        }

        private TcpListener _listener;

        public Connector(IConfiguration configuration, Action<string> showMessage)
        {
            Address = GetAppSettings<string>(configuration, "ServerInfo", "Address");
            Port = GetAppSettings<int>(configuration, "ServerInfo", "Port");

            if (Address == default(string))            
                Address = "127.0.0.1";
            
            if (Port == default(int))            
                Port = 8080;
            
            _listener = new TcpListener(IPAddress.Parse(Address), Port);

            ShowMessage += showMessage;
        }

        private T GetAppSettings<T>(IConfiguration configuration, string section, string key)
        {
            IConfigurationSection serverInfo = configuration.GetSection(section);
            T value;

            try
            {
                value = serverInfo.GetValue<T>(key);
                if (EqualityComparer<T>.Default.Equals(value, default(T)))
                {
                    // value가 T 타입의 기본값인 경우 처리할 내용
                    return default(T);
                }
                else
                {
                    // value가 T 타입의 기본값이 아닌 경우 처리할 내용
                    return value;
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }        

        public async Task StartAsync()
        {
            TcpClient client;
            
            try
            {
                _listener.Start();

                while (true)
                {
                    client = await _listener.AcceptTcpClientAsync();
                    ShowMessage("[클라이언트에서 연결되었음]");

                    // 반한된 네트워크 스트림을 통해서 데이터를 주고 받을 수 있음
                    NetworkStream stream = client.GetStream();

                    while(true)
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void Stop()
        {
        }        
    }
}
