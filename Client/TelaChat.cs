using EI.SI;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class TelaChat : Form
    {
        private const int PORT = 500;
        private NetworkStream networkStream;
        private TcpClient tcpClient;
        private ProtocolSI protocolSI;
        private bool isConnectionOpen = true;
        private HashSet<string> recipients = new HashSet<string>();

        public TelaChat()
        {
            InitializeComponent();
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, PORT);
                tcpClient = new TcpClient();
                tcpClient.Connect(endPoint);
                networkStream = tcpClient.GetStream();
                protocolSI = new ProtocolSI();

                // Iniciar a escuta de mensagens do servidor
                Task.Run(() => ListenForMessages());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao conectar ao servidor: " + ex.Message);
            }
        }

        private void CloseCliente()
        {
            if (isConnectionOpen)
            {
                try
                {
                    byte[] eot = { (byte)ProtocolSICmdType.EOT };
                    networkStream.Write(eot, 0, eot.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao enviar EOT para o servidor: " + ex.Message);
                }
                finally
                {
                    isConnectionOpen = false;
                }
            }
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseCliente();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void TelaChat_Load(object sender, EventArgs e)
        {

        }

        private void AppendMessageToHistory(string senderName, string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string formattedMessage = $"{timestamp} {senderName}: {message}\n";

          /*  richTextBoxHistory.Invoke(new Action(() =>
            {
                richTextBoxHistory.AppendText(formattedMessage);
            }));*/
        }

        private async Task ListenForMessages()
        {
            try
            {
                while (isConnectionOpen)
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        AppendMessageToHistory("Servidor", message);
                    }
                }
            }
            catch (Exception ex)
            {
                if (isConnectionOpen)
                {
                    Console.WriteLine("Erro ao receber mensagem do servidor: " + ex.Message);
                }
            }
        }
    }
}
