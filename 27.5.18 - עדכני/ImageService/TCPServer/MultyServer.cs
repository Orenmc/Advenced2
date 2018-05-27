using Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService
{
    internal class MultyServer
    {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;
        private List<TcpClient> allClients;
        public static Mutex writeMutex = ClientHandler.m;


        public MultyServer(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
            ch.CommandRecieved += SendAllClientCommand;
            this.allClients = new List<TcpClient>();
        }


        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            listener.Start();


            while (true)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    this.allClients.Add(client);
                    Task t = new Task(() =>
                    {
                        ch.HandleClient(client);
                    });
                    t.Start();
                }
                catch (SocketException)
                {
                    break;
                }
            }
        }
        public void Stop()
        {
            listener.Stop();
        }

        public void NewLogArriaved(object sender, MessageRecievedEventArgs mra)
        {
            string[] argument = new string[2];
            argument[0] = mra.Message;
            argument[1] = ((int)mra.Status).ToString();
            CommandRecievedEventArgs e = new CommandRecievedEventArgs((int)CommandStateEnum.GET_NEW_LOG, argument, "");
            string jCommand = JsonConvert.SerializeObject(e);

            List<TcpClient> connectedClient = new List<TcpClient>(this.allClients);

            try
            {

                foreach (TcpClient client in connectedClient)
                {
                    new Task(() =>
                    {
                        try
                        {
                            NetworkStream clientStream = client.GetStream();
                            BinaryWriter write = new BinaryWriter(clientStream);
                            writeMutex.WaitOne();
                            write.Write(jCommand);
                            writeMutex.ReleaseMutex();
                        }
                        catch (Exception)
                        {
                            this.allClients.Remove(client);
                        }
                    }).Start();
                }
            }
            catch (Exception)
            {

            }
        }
        public void SendAllClientCommand(object sender, CommandRecievedEventArgs e) 
        {
            if(e.CommandID == (int)CommandStateEnum.CLOSE_HANDLER)
            {
                string jCommand = JsonConvert.SerializeObject(e);

                List<TcpClient> connectedClient = new List<TcpClient>(this.allClients);

                try
                {
                    foreach (TcpClient client in connectedClient)
                    {
                        new Task(() =>
                        {
                            try
                            {
                                NetworkStream clientStream = client.GetStream();
                                BinaryWriter write = new BinaryWriter(clientStream);
                                writeMutex.WaitOne();
                                write.Write(jCommand);
                                writeMutex.ReleaseMutex();
                            }
                            catch (Exception)
                            {
                                this.allClients.Remove(client);
                            }
                        }).Start();
                    }
                }
                catch (Exception)
                {

                }

            }

        }

    }
}