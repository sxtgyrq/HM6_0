using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6
{
    internal class Listen
    {
        internal static void IpAndPort(string hostIP, int tcpPort)
        {
            var dealWith = new TcpFunction.ResponseC.DealWith(DealWith);
            TcpFunction.ResponseC.f.ListenIpAndPort(hostIP, tcpPort, dealWith);
        }
        private static string DealWith(string notifyJson, int port)
        {

            return "";
        }
    }
}
