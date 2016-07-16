using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Structs
{
    public class OPre_mod
    {
        private int uid;
        private short op_code;

        public short Op_code
        {
            get { return op_code; }
            set { op_code = value; }
        }

        public int Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        public void FromBytes(byte[] data)
        {
            this.uid = BitConverter.ToInt32(data, 0);
            this.op_code = BitConverter.ToInt16(data, 4);
        }
    }
}