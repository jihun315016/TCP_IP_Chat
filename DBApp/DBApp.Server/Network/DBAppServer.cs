using DBApp.Model.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DBApp.Server.Network
{
    public class DBAppServer
    {
        private Action<string> ShowMessage;
        private TcpListener _listener;
        private List<User> _userList;

        public DBAppServer(IConfiguration configuration, Action<string> showMessage)
        {
            string address = "127.0.0.1";
            int port = 8081;
            _listener = new TcpListener(IPAddress.Parse(address), port);
            _userList = new List<User>();
            ShowMessage += showMessage;
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
                    _ = HandleClientAsync(client);
                }
            }
            catch (Exception ex)
            {
                
            }
        }


        private async Task HandleClientAsync(TcpClient client)
        {
            byte[] bufferSize = new byte[4];
            byte[] buffer;
            int read, size;
            string message;
            Data data;
            User user;

            try
            {
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
                    data = JsonSerializer.Deserialize<Data>(message);
                    _userList.Add(new User()
                    {
                        Address = data.Address,
                        Port = data.Port,
                        UserName = data.UserName
                    });
                    ShowMessage($"{data.UserName} 접속");
                }
            }
            catch (Exception ex)
            {

            }
        }


        public static T GetAppSettings<T>(IConfiguration configuration, string section, string key)
        {
            IConfigurationSection appSettingSection = configuration.GetSection(section);
            T value;

            try
            {
                value = appSettingSection.GetValue<T>(key);
                if (EqualityComparer<T>.Default.Equals(value, default))
                {
                    // value가 T 타입의 기본값인 경우 처리할 내용
                    return default;
                }
                else
                {
                    // value가 T 타입의 기본값이 아닌 경우 처리할 내용
                    return value;
                }
            }
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}
