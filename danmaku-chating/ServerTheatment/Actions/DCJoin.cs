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
    public class DCJoin {
        public static async Task<DCJoinResult> DoJoin(string roomKey,string userName) {
            if (userName == "") {
                throw new Exception("EmptyUserName");
            } else if (roomKey == "") {
                throw new Exception("EmptyRoomKey");
            }
            bool s = true;
            Room_mod mod = new Room_mod() {
                User_id = userName,
                Room_key = roomKey,
                Uid = 1
            };

            JoinRoom.Join(mod, 1);
            try {
                mod.FromBytes(SockReceiver.ReceiveData());
            } catch {
                s = false;
            }
            DCJoinResult r = new DCJoinResult() {
                IsSucceed = s,
                RoomName = mod.Room_name,
                UserName = mod.User_id
            };
            return r;
        }
    }
}
