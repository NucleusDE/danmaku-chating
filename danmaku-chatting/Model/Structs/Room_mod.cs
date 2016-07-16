using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Structs
{
    public class Room_mod : ModelBase
    {
        private string room_name = "";
        private string user_id = "";
        private string room_key = "";

        public string Room_key
        {
            get
            {
                return room_key;
            }

            set
            {
                room_key = value;
            }
        }

        public string Room_name
        {
            get
            {
                return room_name;
            }

            set
            {
                room_name = value;
            }
        }

        public string User_id
        {
            get
            {
                return user_id;
            }

            set
            {
                user_id = value;
            }
        }

        public byte[] ToBytes(int uid, short type)
        {
            byte[] bResult = new byte[58];
            BitConverter.GetBytes(type).CopyTo(bResult, 0);
            BitConverter.GetBytes(uid).CopyTo(bResult, 2);
            Encoding.Default.GetBytes(room_name).CopyTo(bResult, 6);
            Encoding.Default.GetBytes(user_id).CopyTo(bResult, 22);
            Encoding.Default.GetBytes(room_key).CopyTo(bResult, 38);
            return bResult;
        }
    }
}
