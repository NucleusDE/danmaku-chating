using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Model.Structs;

namespace ServerThreatment.Results
{
    public class MsgReceiveArgs
    {
        public string Message { get; internal set; }
        public string Sender { get; internal set; }
        public Color Color { get; internal set; }
        public Positions Position { get; internal set; }
    }
}
