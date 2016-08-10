using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerThreatment.Results;
using Model.Structs;
using Invoker.Invokers;
using libNetwork.Sockets;

namespace ServerThreatment.Actions {
    public class DCSendMessage {
        public static async Task SendMessage(string msg, string userName,Positions p ,int color) {
            if (userName == "") {
                throw new Exception("EmptyUserName");
            } else if (msg == "") {
                throw new Exception("EmptyRoomKey");
            }
            Message_mod mod = new Message_mod() {
                Sender = userName,
                StrMessage = msg,
                Position = p,
                Color = color,
            };

            Invoker.Invokers.SendMessage.Send(mod);
        }
    }
}
