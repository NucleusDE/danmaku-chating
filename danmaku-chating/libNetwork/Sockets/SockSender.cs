using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using libData;
using libMemoryData;

namespace libNetwork.Sockets
{
    static public class SockSender
    {
        static public bool SendMessage(byte[] data)
        {
            try
            {
                if (ServerProperty.RemoteServer == null)
                    return false;
                NetworkStream streamToServer = ServerProperty.RemoteServer.GetStream();
                streamToServer.Write(data, 0, data.Length);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}