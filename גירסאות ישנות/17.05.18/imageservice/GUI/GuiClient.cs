using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace GUI
{
    class GuiClient
    {
        private bool isConnected = false;
        private static GuiClient gui;
        private TcpClient client;
        private GuiClient()
        {
            ConnectClientToServer();

        }

        private void ConnectClientToServer()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8004);
            client = new TcpClient();
            client.Connect(ep);
            IsConnected = true;
            Console.WriteLine("You are connected");

            
        }

        public bool IsConnected
        {
            get => this.isConnected;
            set => this.isConnected = value;
        }
        public static GuiClient GetInstance
        {
            get
            {
                if (gui == null)
                {
                    gui = new GuiClient();
                }
                return gui;
            }
        }
        /*********************
         * TODO: add CommandEventArgEvent
         *********************/
        public void SendCommandToServer(CommandStateEnum e)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);
                string test = ((int)e).ToString();
                writer.Write(test);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string RecivedMessageFromServer()
        {

            string s = null;
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            try
            {
                s = reader.ReadString();
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return s;


        }

    }
}
