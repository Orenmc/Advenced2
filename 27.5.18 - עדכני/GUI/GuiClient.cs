using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Infrastructure;
using Newtonsoft.Json;
using static GUI.SettingModal;

namespace GUI
{
    class GuiClient
    {
        private bool isConnected = false;
        public static Mutex writeMutex = new Mutex();
        private static GuiClient gui;
        private TcpClient client;
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;
        private GuiClient()
        {
            ConnectClientToServer();

        }

        private void ConnectClientToServer()
        {
            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8005);
                client = new TcpClient();
                client.Connect(ep);
                IsConnected = true;
            }
            catch (Exception)
            {
                IsConnected = false;
                //MessageBox.Show(e.ToString());

            }
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
        public void SendCommandToServer(CommandRecievedEventArgs e)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);
                BinaryReader reader = new BinaryReader(stream);
                string result = string.Empty;
                new Task(() =>
                {
                    try
                    {
                        string jComman = JsonConvert.SerializeObject(e);
                        writeMutex.WaitOne();
                        writer.Write(jComman);
                        writeMutex.ReleaseMutex();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }).Start();
            }
            catch (Exception)
            {

            }
        }

        public void RecivedMessageFromServer()
        {
            try
            {
                string s = null;
                NetworkStream stream = client.GetStream();
                BinaryReader reader = new BinaryReader(stream);
                new Task(() =>
                {
                    while (true)
                    {

                        try
                        {
                            s = reader.ReadString();
                            stream.Flush();
                            CommandRecievedEventArgs newComman = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(s);
                            this.CommandRecieved?.Invoke(this, newComman);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                    }
                }).Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
