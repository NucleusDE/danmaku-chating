using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerThreatment.Results;
using Model.Structs;
using System.Windows.Media;
using Invoker.Invokers_Rece;

namespace ServerThreatment.Actions
{
    public static class DCMsgReceiveManager
    {
        public delegate void OnMsgReceivedHandle(MsgReceiveArgs args);
        static public event OnMsgReceivedHandle OnMsgReceived;

        public static void Initialize()
        {
            StartListener.MessageCallBack += ReceivedMessage;
        }

        internal static void ReceivedMessage(byte[] data)
        {
            Message_mod mod = new Message_mod();
            mod.FromBytes(data);

            var args = new MsgReceiveArgs() {
                Color = ConvertIntToColor(mod.Color),
                Message = mod.StrMessage,
                Position = mod.Position,
                Sender = mod.Sender,
            };
            OnMsgReceived(args);
        }

        private static Color ConvertIntToColor(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            return Color.FromArgb(bytes[0], bytes[1], bytes[2], bytes[3]);
        }
    }
}
