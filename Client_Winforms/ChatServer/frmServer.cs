using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows.Forms;
using ChatLib;

namespace ChatServer
{
    public partial class frmServer : Form
    {
        private TcpListener _listener;


        public frmServer()
        {
            InitializeComponent();
        }

        private async void btnListen_Click(object sender, EventArgs e)
        {
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
            _listener.Start(); // 127.0.0.1:8080으로 서버 실행
            Console.WriteLine("연결 성공");

            while (true)
            {
                // client가 위 IP와 Port로 연결할 때까지 대기
                // client가 접근하게 되면 다음 코드 진행
                // 아래와 같이 동기적으로 작업하면 메인 스레드 blocking
                // -> 비동기 처리 필요
                // TcpClient client = _listener.AcceptTcpClient();
                TcpClient client = await _listener.AcceptTcpClientAsync();
                Console.WriteLine("누군가 들어옴");

                // await stream.ReadAsync() 메서드에 의해
                // 첫 번째 클라이언트가 응답을 하기 전까지는 두 번째 클라이언트가 접속을 하지 못 함
                // 따라서 클라이언트의 데이터를 수신하고 전송하는 부분을 비동기적으로 빼준다.
                // 비동기로 처리했기 때문에 값이 들어오지 않더라도 다음 실행으로 넘어가서 다음 클라리언트를 대기할 수 있음

                // await 없이 비동기적으로 호출
                // 클라이언트가 접속을 하게 되면 
                _ = HandleClient(client);
            }
        }

        private async Task HandleClient(TcpClient client)
        {
            // 반환된 네트워크 스트림을 통해서 데이터를 주고 받을 수 있음
            NetworkStream stream = client.GetStream();

            // Client에서 처음 넘어오는 messageLengthBuffer의 크기는 4byte
            byte[] sizeBuffer = new byte[4];
            int read;

            // read = await stream.ReadAsync(buffer, 0, buffer.Length)
            // 클라이언트에서 메시지를 주기 전까지 대기
            // buffer에 0 ~ buffer.length까지 읽어들임

            // read 값이 0이면 정상적인 데이터가 아니라고 판단
            // read에 값을 할당하고 그 값이 0보다 큰 동안 반복
            //while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            while (true)
            {
                read = await stream.ReadAsync(sizeBuffer, 0, sizeBuffer.Length);
                if (read == 0)
                    break;

                // size는 다음에 넘어올 메시지의 크기가 될 것
                int size = BitConverter.ToInt32(sizeBuffer, 0);

                byte[] buffer = new byte[size];
                read = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (read == 0)
                    break;

                Console.WriteLine($"무언가 읽었음");

                // 읽어들인 read를 텍스트로 변환
                string message = Encoding.UTF8.GetString(buffer, 0, read);

                var hub = ChatHub.Parse(message);
                
                listBox1.Items.Add($"UserId : {hub.UserId}, RoomId : {hub.RoomId}," +
                                   $"UserName : {hub.UserName}, Message : {hub.Message}"
                    );

                txtCount.Text = listBox1.Items.Count.ToString();
                var messageBuffer = Encoding.UTF8.GetBytes($"Server : {message}");
                stream.Write(messageBuffer, 0, messageBuffer.Length);
            }
        }
    }
}
