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
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
              //  string s = reader.ReadLine();
                // int num = Int32.Parse(s);

                /**********************************************/
                //loggingModel.Log(s, MessageTypeEnum.FAIL);
                /**********************************************/
                //int num = 2;

                //num = 10;

            
                //use the controller to execute
                //for new File command taks the args[0].
                string msg = imageController.ExecuteCommand(2, null, out bool result, out MessageTypeEnum type);

                //log write the result msg, with the returning type
               // loggingModel.Log(msg, type);

                writer.Write(msg);

            }
        }
    }
}