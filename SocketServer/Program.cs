using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
    class Program
    {

        static void Main(string[] args)
        {
            IPHostEntry ipHostEntry =
                Dns.GetHostEntry("localhost");

            IPAddress iPAddress = ipHostEntry.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(iPAddress, 5001);
            Console.WriteLine("Awaiting messages from client");
            Socket listener = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


            try
            {
                listener.Bind(ipEndPoint);
                listener.Listen(10);

                while(true)
                {
                    Socket handler = listener.Accept();
                    string data = null;

                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);

                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    Console.WriteLine($"receive: {data}");

                    string reply = $"Data received: {DateTime.Today}";
                    byte[] replyMsg = Encoding.UTF8.GetBytes(reply);

                    handler.Send(replyMsg);
                    
                    if(data.IndexOf("<eof>") > -1)
                    {
                        Console.WriteLine("Server closed connection with client");
                        break;
                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            } 

        }
    }
}
