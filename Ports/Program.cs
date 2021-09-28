using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Ports
{
    class Program
    {
        static void Main(string[] args)
        {
            IPGlobalProperties globalProp = IPGlobalProperties.GetIPGlobalProperties();

            TcpConnectionInformation[] connections = globalProp.GetActiveTcpConnections();

            foreach(TcpConnectionInformation item in connections)
            {
                StringBuilder builder = new StringBuilder();

                builder.Append($"Local endpoind: \t{item.LocalEndPoint.Address.ToString()}")
                    .Append($"\nRemote Port: {item.RemoteEndPoint.Port.ToString()}")
                    .Append($"\nState\t\t{item.State.ToString()}");
                    ;

                Console.WriteLine(builder.ToString());


            } 

        }
    }
}
