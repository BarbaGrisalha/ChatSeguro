using EI.SI;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class TelaChat : Form
    {
        private const int PORT = 500; // Porta para conexão com o servidor.
        private NetworkStream networkStream; // Stream para enviar e receber dados.
        private TcpClient tcpClient; // Cliente TCP.
        private ProtocolSI protocolSI; // Instância do protocolo.
        private bool isConnectionOpen = true; // Indicador de conexão aberta.

        public TelaChat()
        {
            InitializeComponent(); // Inicializa os componentes do formulário.
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, PORT); // Define o ponto de extremidade.
                tcpClient = new TcpClient(); // Inicializa o cliente TCP.
                tcpClient.Connect(endPoint); // Conecta ao servidor.
                networkStream = tcpClient.GetStream(); // Obtém o stream de rede.
                protocolSI = new ProtocolSI(); // Inicializa o protocolo.

                // Inicia a escuta de mensagens do servidor de forma assíncrona.
                Task.Run(() => ListenForMessages());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao conectar ao servidor: " + ex.Message); // Exibe erro de conexão.
            }
        }

        private void CloseCliente()
        {
            if (isConnectionOpen)
            {
                try
                {
                    byte[] eot = protocolSI.Make(ProtocolSICmdType.EOT); // Define o comando de fim de transmissão.
                    networkStream.Write(eot, 0, eot.Length); // Envia o comando para o servidor.
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao enviar EOT para o servidor: " + ex.Message); // Exibe erro ao enviar EOT.
                }
                finally
                {
                    isConnectionOpen = false; // Marca a conexão como fechada.
                }
            }
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            Close(); // Fecha o formulário.
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseCliente(); // Fecha o cliente ao fechar o formulário.
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Evento do clique no label2 (vazio).
        }

        private void TelaChat_Load(object sender, EventArgs e)
        {
            // Evento de carregamento do formulário (vazio).
        }

        private void AppendMessageToHistory(string senderName, string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Obtém o timestamp atual.
            string formattedMessage = $"{timestamp} {senderName}: {message}\n"; // Formata a mensagem.

            textBox1_Message.Invoke(new Action(() =>
            {
                textBox1_Message.AppendText(formattedMessage); // Adiciona a mensagem ao histórico.
            }));
        }

        private async Task ListenForMessages()
        {
            try
            {
                while (isConnectionOpen)
                {
                    byte[] buffer = new byte[1024]; // Buffer para armazenar os dados recebidos.
                    int bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length); // Lê os dados do servidor.
                    if (bytesRead > 0)
                    {
                        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead); // Converte os dados em string.
                        AppendMessageToHistory("Servidor", message); // Adiciona a mensagem ao histórico.
                    }
                }
            }
            catch (Exception ex)
            {
                if (isConnectionOpen)
                {
                    Console.WriteLine("Erro ao receber mensagem do servidor: " + ex.Message); // Exibe erro ao receber mensagem.
                }
            }
        }

        // Torne o método assíncrono para poder usar await nele
        private async void buttonSend_ClickAsync(object sender, EventArgs e)
        {
            // Obtém o texto da mensagem digitada pelo usuário.
            string message = textBoxSendMessenger.Text;
            textBoxSendMessenger.Clear();

            // Verifica se a mensagem não está vazia.
            if (!string.IsNullOrEmpty(message))
            {
                try
                {
                    // Cria o pacote de dados para enviar ao servidor.
                    byte[] packet = protocolSI.Make(ProtocolSICmdType.DATA, message);
                    // Envia os dados ao servidor de forma assíncrona.
                    await networkStream.WriteAsync(packet, 0, packet.Length);

                    // Aguarda a confirmação (ACK) do servidor de forma assíncrona.
                    while (protocolSI.GetCmdType() != ProtocolSICmdType.ACK)
                    {
                        await networkStream.ReadAsync(protocolSI.Buffer, 0, protocolSI.Buffer.Length);
                    }

                    // Adiciona a mensagem ao histórico com o remetente "Você".
                    AppendMessageToHistory("Você", message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao enviar mensagem: " + ex.Message); // Exibe erro ao enviar mensagem.
                }
            }
        }
    }
}
