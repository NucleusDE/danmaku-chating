using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Invoker.Invokers_Rece
{
    public class AnalysisStack
    {
        public delegate void CallBackFuncs(byte[] data);
        private static Dictionary<int, CallBackFuncs> AnalysisList = new Dictionary<int, CallBackFuncs>();

        public static void Insert(int index, CallBackFuncs func)
        {
            if (AnalysisList.ContainsKey(index))
                throw new Exception("The index was existed");
            AnalysisList.Add(index, func);
        }

        public static void Clear()
        {
            AnalysisList.Clear();
        }

        public static void Remove(int index)
        {
            if(!AnalysisList.ContainsKey(index))
                throw new Exception("The index was invaid");
            AnalysisList.Remove(index);
        }

        public static void Invoker(byte[] data)
        {
            int uid = BitConverter.ToInt32(data, 2);

            if (!AnalysisList.ContainsKey(uid))
                throw new Exception("The index was invaid");
            AnalysisList[uid](data);
            Remove(uid);
        }

        private static int index = 0;

        public static int Index {
            get {
                index++;   
                return index;
            }
        }
    }
} 