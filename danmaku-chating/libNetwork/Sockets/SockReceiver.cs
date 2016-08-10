using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using libMemoryData;

namespace libNetwork.Sockets
{
    static public class SockReceiver
    {

        const int BufferSize = 1452;
        static public byte[] ReceiveData()
        {
            try
            {
                if (ServerProperty.RemoteServer == null)
                    throw new Exception("The server hasn't been connected");             
                NetworkStream streamToClient = ServerProperty.RemoteServer.GetStream();
                byte[] buffer = new byte[BufferSize];
                int bytesRead = streamToClient.Read(buffer, 0, BufferSize);

                int correctSize = 0;
                while (buffer[correctSize] != 0)
                {
                    correctSize++;
                }
                byte[] correctBuffer = new byte[correctSize];
                Buffer.BlockCopy(buffer, 0, correctBuffer, 0, correctSize);

                return buffer;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
