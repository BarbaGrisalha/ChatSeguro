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
                    byte[] eot = protocolSI.Make(ProtocolSICmdType.EOT);
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

        private void AppendMessageToHistory(string senderName, string message,string receptor)
        {
            tbNomeReceptor.Text = receptor; // Corrigido: tbNomeReceptor é um controle TextBox
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string formattedMessage = $"{timestamp} {senderName}: {message} to {receptor}\n"; // Corrigido: uso correto de receptor
            //richTextBoxHistory.Invoke(new Action(() =>
            //{
           //     richTextBoxHistory.AppendText(formattedMessage);
            //}));
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
                        AppendMessageToHistory("Servidor", message, "Todos"); // Adicionei um receptor genérico
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

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string userName = tbNomeEmissor.Text;
            string receptor = tbNomeReceptor.Text;

            if (string.IsNullOrWhiteSpace(textBoxSendMessenger.Text))
            {
                MessageBox.Show("Por favor, insira uma mensagem para enviar.");
                return;
            }

            try
            {
                string message = textBoxSendMessenger.Text;
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd;HH:mm:ss");
                string formattedMessage = $"{timestamp}<{userName}> \"{message}\"";

                byte[] data = protocolSI.Make(ProtocolSICmdType.DATA, formattedMessage);
                networkStream.Write(data, 0, data.Length);

                AppendMessageToHistory(userName, message, receptor);

                textBoxSendMessenger.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao enviar a mensagem: " + ex.Message);
            }
        }

        private void textBoxSendMessenger_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbNomeReceptor_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
