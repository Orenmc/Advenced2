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
        public string SendCommandToServer(CommandStateEnum e)
        {
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                int t2 = (int)e;
                string t1 = t2.ToString();
             //   writer.Write(t1);
                int n1 = 2 + 2;
                string s = reader.ReadLine();
                return s;
            }
        }

    }
}
