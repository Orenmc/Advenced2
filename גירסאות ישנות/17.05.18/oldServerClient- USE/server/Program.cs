using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    class Program
    {
        static void Main(string[] args)
        {


            IClientHandler ch = new ClientHandler();
            MultyServer server = new MultyServer(8004, ch);

            Task t = new Task(() =>
            {
                server.Start();
            });
            t.Start();

            while (true)
            {
                Console.WriteLine("press exit");

                string s = Console.ReadLine();
                if (String.Compare(s, "exit") == 0)
                {
                    break;


                }
                Console.WriteLine("get here?? server?");

                server.Stop();
            }
        }
    }
}