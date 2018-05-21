using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure;
using Newtonsoft.Json;

namespace ImageService
{
    class ClientHandler : IClientHandler
    {
        private IImageController imageController;
        private ILoggingModel loggingModel;
        public static Mutex m = new Mutex();
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;


        public ClientHandler(IImageController ic, ILoggingModel lm)
        {
            imageController = ic;
            loggingModel = lm;
           
        }

        public void HandleClient(TcpClient client)
        {
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {


                while (true)
                {
                    string[] argument = new string[2];
                    string newcommand = reader.ReadString();
                    CommandRecievedEventArgs command = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(newcommand);
                    if (command.CommandID == (int)CommandStateEnum.CLOSE_HANDLER)
                    {
                        CommandRecieved?.Invoke(this, command);
                    }    
                    string msg = imageController.ExecuteCommand((int)command.CommandID, null, out bool result, out MessageTypeEnum type);
                    m.WaitOne();
                    writer.Write(msg);
                    m.ReleaseMutex();
                }

               // client.Close();
            }
        }
    }
}