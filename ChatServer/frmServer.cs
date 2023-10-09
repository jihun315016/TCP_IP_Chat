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
            _listener.Start(); // 127.0.0.1:8080���� ���� ����
            Console.WriteLine("���� ����");

            while (true)
            {
                // client�� �� IP�� Port�� ������ ������ ���
                // client�� �����ϰ� �Ǹ� ���� �ڵ� ����
                // �Ʒ��� ���� ���������� �۾��ϸ� ���� ������ blocking
                // -> �񵿱� ó�� �ʿ�
                // TcpClient client = _listener.AcceptTcpClient();
                TcpClient client = await _listener.AcceptTcpClientAsync();
                Console.WriteLine("������ ����");

                // await stream.ReadAsync() �޼��忡 ����
                // ù ��° Ŭ���̾�Ʈ�� ������ �ϱ� �������� �� ��° Ŭ���̾�Ʈ�� ������ ���� �� ��
                // ���� Ŭ���̾�Ʈ�� �����͸� �����ϰ� �����ϴ� �κ��� �񵿱������� ���ش�.
                // �񵿱�� ó���߱� ������ ���� ������ �ʴ��� ���� �������� �Ѿ�� ���� Ŭ�󸮾�Ʈ�� ����� �� ����

                // await ���� �񵿱������� ȣ��
                // Ŭ���̾�Ʈ�� ������ �ϰ� �Ǹ� 
                _ = HandleClient(client);
            }
        }

        private async Task HandleClient(TcpClient client)
        {
            // ��ȯ�� ��Ʈ��ũ ��Ʈ���� ���ؼ� �����͸� �ְ� ���� �� ����
            NetworkStream stream = client.GetStream();

            // Client���� ó�� �Ѿ���� messageLengthBuffer�� ũ��� 4byte
            byte[] sizeBuffer = new byte[4];
            int read;

            // read = await stream.ReadAsync(buffer, 0, buffer.Length)
            // Ŭ���̾�Ʈ���� �޽����� �ֱ� ������ ���
            // buffer�� 0 ~ buffer.length���� �о����

            // read ���� 0�̸� �������� �����Ͱ� �ƴ϶�� �Ǵ�
            // read�� ���� �Ҵ��ϰ� �� ���� 0���� ū ���� �ݺ�
            //while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            while (true)
            {
                read = await stream.ReadAsync(sizeBuffer, 0, sizeBuffer.Length);
                if (read == 0)
                    break;

                // size�� ������ �Ѿ�� �޽����� ũ�Ⱑ �� ��
                int size = BitConverter.ToInt32(sizeBuffer, 0);

                byte[] buffer = new byte[size];
                read = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (read == 0)
                    break;

                Console.WriteLine($"���� �о���");

                // �о���� read�� �ؽ�Ʈ�� ��ȯ
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
