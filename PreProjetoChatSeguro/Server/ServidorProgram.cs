using EI.SI;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    internal class ServidorProgram
    {
        private const int PORT = 500; //DEFINIMOS O PORTO PARA UTILIZAR

        static void Main(string[] args)
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, PORT);
                TcpListener listener = new TcpListener(endPoint);

                listener.Start(); // Servidor a escuta
                Console.WriteLine("SERVER READY");
                int clientCounter = 0;

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    clientCounter++;
                    Console.WriteLine("Client {0} connected.", clientCounter);
                    ClientHandler clientHandler = new ClientHandler(client, clientCounter);
                    clientHandler.Handle();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro no servidor: " + ex.Message);
            }
        }
    }

    class ClientHandler
    {
        private TcpClient client;
        private int clientID;

        public ClientHandler(TcpClient client, int clientID)
        {
            this.client = client;
            this.clientID = clientID;
        }

        public void Handle()
        {
            Thread thread = new Thread(threadHandler);
            thread.Start();
        }

        private void threadHandler()
        {
            try
            {
                NetworkStream networkStream = this.client.GetStream();
                ProtocolSI protocolSI = new ProtocolSI();

                bool isEndOfTransmission = false; // Variável para controlar o fim da transmissão

                while (!isEndOfTransmission)
                {
                    int bytesRead = networkStream.Read(protocolSI.Buffer, 0, protocolSI.Buffer.Length);
                    byte[] ack;

                    switch (protocolSI.GetCmdType())
                    {
                        case ProtocolSICmdType.DATA:
                            string receivedMessage = protocolSI.GetStringFromData();
                            Console.WriteLine("Client " + clientID + " sent: " + receivedMessage);

                            ack = protocolSI.Make(ProtocolSICmdType.ACK);
                            networkStream.Write(ack, 0, ack.Length);
                            break;
                        case ProtocolSICmdType.EOT:
                            Console.WriteLine("Ending Thread from Client {0}", clientID);
                            ack = protocolSI.Make(ProtocolSICmdType.ACK);
                            networkStream.Write(ack, 0, ack.Length);

                            // Definir a variável para indicar o fim da transmissão
                            isEndOfTransmission = true;
                            break;
                    }
                }

                networkStream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro no tratamento do cliente: " + ex.Message);
            }
        }
    }
}
