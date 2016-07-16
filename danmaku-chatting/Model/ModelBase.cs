using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ModelBase
    {
        private int uid;

        public int Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        public void FromBytes(byte[] data)
        {

        }
    }
}
