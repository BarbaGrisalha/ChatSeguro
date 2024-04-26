using EI.SI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        private const int PORT = 500; //DEFINIMOS O PORTO PARA UTILIZAR
        static void Main(string[] args)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, PORT);
            TcpListener listener = new TcpListener(endPoint);

            listener.Start();// Servidor a escuta 
            Console.WriteLine("SERVER READY");
            int clientCounter = 0;

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                clientCounter++;
                Console.WriteLine("Client {0} connected.", clientCounter);
                ClientHandler clientHandler = new ClientHandler(client,clientCounter);
                clientHandler.Handle();
            }
        }
    }
    class ClientHandler {
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
            NetworkStream networkStream = this.client.GetStream();
            ProtocolSI protocolSI = new ProtocolSI();
            while (protocolSI.GetCmdType() != ProtocolSICmdType.EOT) 
            {
                int bytesRead = networkStream.Read(protocolSI.Buffer, 0, protocolSI.Buffer.Length);
                byte[] ack;
                switch(protocolSI.GetCmdType())
                {
                    case ProtocolSICmdType.DATA:
                        string receivedMessage = protocolSI.GetStringFromData();
                        //Console.WriteLine("Client " + clientID + " : " + protocolSI.GetStringFromData());
                        Console.WriteLine("Client " + clientID + " sent: " + receivedMessage); // Esta é a linha adicionada

                        ack = protocolSI.Make(ProtocolSICmdType.ACK);
                        networkStream.Write(ack, 0, ack.Length);
                        break;
                    case ProtocolSICmdType.EOT:
                        Console.WriteLine("Ending Thread from Client {0}", clientID);
                        ack = protocolSI.Make(ProtocolSICmdType.ACK);
                        networkStream.Write(ack, 0, ack.Length);
                        break;
                }
            }
            networkStream.Close();
            client.Close();
        }
    }
}
