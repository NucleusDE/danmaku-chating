using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Structs
{
    public class Message_mod
    {
        short color;
        short position;
        string sender = "";
        string strMessage = "";

        public short Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public short Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public string Sender
        {
            get
            {
                return sender;
            }

            set
            {
                sender = value;
            }
        }

        public string StrMessage
        {
            get
            {
                return strMessage;
            }

            set
            {
                strMessage = value;
            }
        }

        public void FromBytes(byte[] data)
        {
            this.color = BitConverter.ToInt16(data, 2);
            this.position = BitConverter.ToInt16(data, 4);

            string temp = Encoding.Default.GetString(data, 6, 16);
            foreach (char c in temp)
            {
                if (c == '\0')
                    break;
                this.sender += c;
            }

            temp = Encoding.Default.GetString(data, 22, 20);
            foreach(char c in temp)
            {
                if (c == '\0')
                    break;
                this.strMessage += c;
            }
        }

        public byte[] ToBytes(byte[] data)
        {
            byte[] bResult = new byte[42];
            BitConverter.GetBytes((short)1).CopyTo(bResult, 0);
            BitConverter.GetBytes(color).CopyTo(bResult, 2);
            BitConverter.GetBytes(position).CopyTo(bResult, 4);
            Encoding.Default.GetBytes(sender).CopyTo(bResult, 6);
            Encoding.Default.GetBytes(strMessage).CopyTo(bResult, 22);
            return bResult;
        }
    }
}
