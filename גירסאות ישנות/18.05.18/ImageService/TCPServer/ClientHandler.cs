using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace ImageService
{
    class ClientHandler : IClientHandler
    {
        private IImageController imageController;
        private ILoggingModel loggingModel;

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
                    //Console.WriteLine("Waiting for a number");
                    string num_command = reader.ReadString();
                    //Console.WriteLine("Number accepted");
                    int command = Int32.Parse(num_command);

                    string msg = imageController.ExecuteCommand(command, null, out bool result, out MessageTypeEnum type);
                    writer.Write(msg);
                }

                client.Close();
            }
        }
    }
}