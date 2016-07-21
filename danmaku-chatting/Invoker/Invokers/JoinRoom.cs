using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Structs;
using libNetwork.Sockets;

namespace Invoker.Invokers
{
    static public class JoinRoom
    {
        static public void Join(Room_mod data, int uid)
        {
            SockSender sender = new SockSender();
            sender.SendMessage(data.ToBytes(uid, 4));
        }
    }
}
