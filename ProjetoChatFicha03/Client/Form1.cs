using EI.SI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
   
    public partial class Form1 : Form
    {
        private const int PORT = 500; //DEFINIMOS O PORTO PARA UTILIZAR
        NetworkStream networkStream;
        TcpClient tcpClient;
        ProtocolSI protocolSI;
        private bool isConnectionOpen = true;/// <summary>
                                             ///Variável para verificação da conexão. Estava dando erro e então 
                                             ///lancei essa variável para verificação da conexão aberta antes de fechar.
                                             /// </summary>

        public Form1()
        {
            InitializeComponent();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, PORT);
            tcpClient = new TcpClient();
            tcpClient.Connect(endPoint);
            networkStream = tcpClient.GetStream();
            protocolSI = new ProtocolSI();
        }

        private async void buttonSend_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = textBox1_Message.Text;
                textBox1_Message.Clear();
                byte[] packet = protocolSI.Make(ProtocolSICmdType.DATA, msg);
                networkStream.Write(packet, 0, packet.Length);

                // Aguardar um ACK do servidor em uma thread separada
                await Task.Run(() =>
                {
                    while (true)
                    {
                        byte[] buffer = new byte[1024]; // Buffer para armazenar a resposta do servidor
                        int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
                        string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                        if (response.Trim() == "ACK") // Comparar com o ACK esperado
                        {
                            Console.WriteLine("Mensagem enviada com sucesso para o servidor!");
                            break; // Sai do loop quando recebe um ACK
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar mensagem para o servidor: " + ex.Message);
            }
        }




        private void CloseCliente()
        {
            if (isConnectionOpen)
            {
                byte[] eot = protocolSI.Make(ProtocolSICmdType.EOT);
                networkStream.Write(eot, 0, eot.Length);
                networkStream.Read(protocolSI.Buffer, 0, protocolSI.Buffer.Length);
                networkStream.Close();
                tcpClient.Close();
                isConnectionOpen = false;//Aqui marcamos a conexão como fechada.
            }
            
            
            
            networkStream.Close();
            tcpClient.Close();
        }
        private void buttonSair_Click(object sender, EventArgs e)
        {
            CloseCliente();
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseCliente();
        }
    }
}
