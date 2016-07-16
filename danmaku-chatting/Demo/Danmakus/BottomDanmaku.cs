using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Media;

namespace libDanmaku.Danmakus
{
    internal class BottomDanmaku : DanmakuWindow
    {
        private readonly int DANMAKU_TOP;
        private readonly int DANMAKU_TIME = 7;
        public int level { get; set; }
        
        public BottomDanmaku(string text, string sender, int top, DanmakuManager pDanmakuManager, int pLevel, Color color) : base(text, pDanmakuManager, sender, color) {
            DANMAKU_TOP = top;
            this.Top = top;
            this.Left = (mDanmakuManager.SCREEN_WIDGH + this.ActualWidth) / 2;
            level = pLevel;
            Thread t = new Thread(WaitDanmaku);
            t.Start();
        }
        private void WaitDanmaku(){
            Thread.Sleep(DANMAKU_TIME * 1000);
            this.Dispatcher.Invoke(() =>
            {
                mDanmakuManager.SetBottomLevelAvaliable(level);
                this.Close();
            }
                );
        }
    }
}
