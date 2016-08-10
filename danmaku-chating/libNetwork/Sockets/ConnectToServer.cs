using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using libData;
using libMemoryData;

namespace libNetwork.Sockets
{
    static public class ConnectToServer
    {
        static public bool Connect()
        {
            try
            {
                ServerProperty.RemoteServer = new TcpClient();
                ServerProperty.RemoteServer.Connect(new System.Net.IPEndPoint(ConstData.ServerIP, ConstData.ServerPort));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
