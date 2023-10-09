using ChatLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class frmClient : Form
    {
        private TcpClient _client;

        public frmClient()
        {
            InitializeComponent();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            _client = new TcpClient();
            await _client.ConnectAsync(IPAddress.Parse("127.0.0.1"), 8080);
            _ = HandleClient(_client);
        }

        private async Task HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int read;

            // 메시지 전송 후 다시 서버에서 응답이 올 때까지 대기
            while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, read);
                listBox1.Items.Add(message);
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            // 연결된 _client의 stream을 이용하여 송수신
            NetworkStream stream = _client.GetStream();

            // 스트림을 이용하여 텍스트 전송
            string text = textBox1.Text;

            ChatHub hub = new ChatHub()
            {
                UserId = 1,
                RoomId = 1,
                UserName = "까불이코더",
                Message = text,
            };

            // text를 바이트 배열로 넣기
            var messageBuffer = Encoding.UTF8.GetBytes(hub.ToJsonString());
            var messageLengthBuffer = BitConverter.GetBytes(messageBuffer.Length);

            // 아래와 같이 한꺼번에 데이터가 몰려오게 되면
            // ReadAsync가 처리하기 전에 데이터가 쭉 쌓인 상태로 데이터를 송신하게 됌
            // 그래서 지금까지 받아진 버퍼를 한꺼번에 읽다보니까 1만 입력했는데
            // 1111111111111111.... 이렇게 1이 쭉 붙어서 전송이 되는 현상이 일어난다.
            // 해결 방법 1
            // > 문자열 구분자 추가해주고 그것을 구분자로 사용한다.
            // >> string text = textBox1.Text + "[END]";
            // >> 구현 간단 / 데이터 크기가 byte 배열을 넘어가거나 사용자가 구분자를 입력할 경우 데이터가 잘리거나 잘못된 데이터 전송
            // >> == 서버에 설정해놓은 크기보다 더 큰 데이터 값이 넘어온 경우
            // 해결 방법 2
            // 전송할 데이터 크기를 미리 보내고, 그 다음 메시지를 전송한다. <- 이 방법 사용할 것
            //for (int i = 0; i < 100; i++) 
            //{
                // 메시지 크기를 먼저 넘기고, 그 다음에 메시지를 넘기는 방식으로 코드를 작성하여
                // 동적으로 버퍼 크기를 생성하기 때문에 버퍼 크기에 대한 제약을 없애준다.
                // >> 불필요한 메모리가 낭비되지 않는다.
                // >> 단점은 두 번 전송하기 때문에 한 번 전송하는 것 보단 서버에 트래픽이 증가한다.
                stream.Write(messageLengthBuffer, 0, messageLengthBuffer.Length);
                stream.Write(messageBuffer, 0, messageBuffer.Length);
            //}
        }
    }
}
