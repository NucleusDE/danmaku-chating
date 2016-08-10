using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Structs;
using libNetwork.Sockets;
using Invoker.Invokers_Rece;

namespace Invoker.Invokers
{
    static public class ExitRoom
    {
        static public void Exit(Room_mod data)
        {
            SockSender.SendMessage(data.ToBytes(AnalysisStack.Index, 3));
        }
    }
}
