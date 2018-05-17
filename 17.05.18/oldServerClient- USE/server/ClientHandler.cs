using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace Connection
{
    class ClientHandler : IClientHandler
    {

        public void HandleClient(TcpClient client)
        {

            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                int num = reader.ReadInt32();
                Console.WriteLine(num);


                /*
                while (true)
                {
                    Console.WriteLine("Waiting for a number");
                    int num = reader.ReadInt32();
                    Console.WriteLine("Number accepted");
                    num *= 2;
                    writer.Write(num);
                }

                client.Close();
*/
            }
        }
    }
}