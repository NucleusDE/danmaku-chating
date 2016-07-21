﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Structs {
    public class Message_mod {
        int color;
        Positions position;
        string sender = "";
        string strMessage = "";

        public int Color {
            get {
                return color;
            }

            set {
                color = value;
            }
        }

        public Positions Position {
            get {
                return position;
            }

            set {
                position = value;
            }
        }

        public string Sender {
            get {
                return sender;
            }

            set {
                sender = value;
            }
        }

        public string StrMessage {
            get {
                return strMessage;
            }

            set {
                strMessage = value;
            }
        }

        public void FromBytes(byte[] data) {
            this.color = BitConverter.ToInt32(data, 2);
            this.position = (Positions)BitConverter.ToInt16(data, 6);

            string temp = Encoding.Default.GetString(data, 8, 16);
            foreach (char c in temp) {
                if (c == '\0')
                    break;
                this.sender += c;
            }

            temp = Encoding.Default.GetString(data, 24, 40);
            foreach (char c in temp) {
                if (c == '\0')
                    break;
                this.strMessage += c;
            }
        }

        public byte[] ToBytes() {
            byte[] bResult = new byte[42];
            BitConverter.GetBytes((short)1).CopyTo(bResult, 0);
            BitConverter.GetBytes(color).CopyTo(bResult, 2);
            BitConverter.GetBytes((int)position).CopyTo(bResult, 6);
            Encoding.Default.GetBytes(sender).CopyTo(bResult, 8);
            Encoding.Default.GetBytes(strMessage).CopyTo(bResult, 24);
            return bResult;
        }
    }
    public enum Positions {
        Top,
        Bottom,
        Move,
    }
}
