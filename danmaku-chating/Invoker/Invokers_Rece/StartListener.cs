using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using libNetwork.Sockets;
using libMemoryData;

namespace Invoker.Invokers_Rece
{
    public static class StartListener
    {
        public delegate void CBMessage(byte[] data);

        private static Thread tdListener;
        static public event CBMessage MessageCallBack;

        public static void Start()
        {
            tdListener = new Thread(delegate () 
            {
                try {
                    ConnectToServer.Connect();

                    while (true) {
                        byte[] data = SockReceiver.ReceiveData();

                        switch (BitConverter.ToInt16(data, 0)) {
                            case 0:
                                Thread.CurrentThread.Abort();
                                break;
                            case 1: //message
                                try {
                                    MessageCallBack(data);
                                } catch (Exception ex) {
                                    throw ex;
                                }
                                break;
                            case 2:
                                AnalysisStack.Invoker(data);
                                break;
                        }
                    }
                } catch {

                }
            });
        }

        public static void Stop()
        {
            tdListener.Abort();
        }
    }
}
