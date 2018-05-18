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
            while (true)
            {
                string msg = null;
                try
                {
                    NetworkStream stream = client.GetStream();
                    BinaryReader reader = new BinaryReader(stream);
                    BinaryWriter writer = new BinaryWriter(stream);

                    /*read from client*/
                    string ans = reader.ReadString();
                    int test = Int32.Parse(ans);

                    msg = imageController.ExecuteCommand(test, null, out bool result, out MessageTypeEnum type);


                    /*write to client*/
                    writer.Write(msg);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            }
            
        }
    }
}