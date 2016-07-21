using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Structs;
using libNetwork.Sockets;

namespace Invoker.Invokers
{
    static public class SendMessage
    {
        static public void Send(Message_mod data)
        {
            SockSender sender = new SockSender();
            sender.SendMessage(data.ToBytes());
        }
    }
}
