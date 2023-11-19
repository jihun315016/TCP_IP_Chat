using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChatService.Client
{
    public class ChatClient
    {
        private ConnectionState _connectionState;

        private readonly string _address;
        private readonly int _port;

        private NetworkStream _stream;

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
                _stream = client.GetStream();

                _ = HandleClientAsync();
            }
            catch (Exception ex)
            {
                
            }
        }


        private async Task HandleClientAsync()
        {
            byte[] bufferSize = new byte[4];
            byte[] buffer;
            int read, size;
            string message;
            Data data;

            try
            {
                while (true)
                {
                    // 메시지를 읽을때까지 대기
                    // buffer1에 0 ~ buffer1.Length까지 읽어들임
                    read = await _stream.ReadAsync(bufferSize, 0, bufferSize.Length);
                    if (read == 0) break;

                    // BitConverter : 바이트 배열과 데이터간 변환 수행
                    // ex)
                    // byte[] byteArray = { 0x78, 0x56, 0x34, 0x12 };
                    // int intValue = BitConverter.ToInt32(byteArray, 0);
                    // Console.WriteLine(intValue);
                    size = BitConverter.ToInt32(bufferSize, 0);
                    buffer = new byte[size];

                    read = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    if (read == 0) break;

                    message = Encoding.UTF8.GetString(buffer);
                    data = JsonSerializer.Deserialize<Data>(message);
                }
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
                Console.WriteLine(ex.Message);
            }
        }
    }
}
