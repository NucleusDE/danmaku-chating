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
    public class DCLogin {
        public static async Task<DCLoginResult> DoLogin(string userName, string roomName) {
            if (userName == "") {
                throw new Exception("EmptyUserName");
            } else if (roomName == "") {
                throw new Exception("EmptyRoomName");
            }
            SockReceiver receiver = new SockReceiver();
            bool s = true;
            Room_mod mod = new Room_mod() {
                Room_name = userName,
                User_id = userName,
                Uid = 1
            };

            CreateRoom.Create(mod, 1);
            try {
                mod.FromBytes(receiver.ReceiveData());
            } catch {
                s = false;
            }
            DCLoginResult r = new DCLoginResult() {
                IsSucceed = s,
                RoomKey = mod.Room_key,
                UserName = mod.User_id
            };
            return r;
        }
    }
}
