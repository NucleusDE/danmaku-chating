using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows;

namespace libDanmaku.Danmakus {
    public class MoveDanmaku : DanmakuWindow {
        private readonly int DANMAKU_TOP;
        private readonly int DANMAKU_TIME = 10;
        public int level { get; set; }

        public MoveDanmaku(string text, string sender, int top, DanmakuManager pDanmakuManager, int pLevel, Color color) : base(text, pDanmakuManager, sender, color) {
            DANMAKU_TOP = top;
            level = pLevel;
            this.Loaded += Load;
        }
        public void Load(object sender, RoutedEventArgs e)
        {
            base.Window_Loaded(sender, e);
            this.Left = mDanmakuManager.SCREEN_WIDGH;
            this.Top = DANMAKU_TOP;

            DoubleAnimation showAnimation = new DoubleAnimation();
            showAnimation.From = mDanmakuManager.SCREEN_WIDGH;
            showAnimation.To = mDanmakuManager.SCREEN_WIDGH - this.ActualWidth;
            showAnimation.Duration = TimeSpan.FromSeconds(Width / (Width + mDanmakuManager.SCREEN_WIDGH) * DANMAKU_TIME);
            showAnimation.Completed += MovedIn;
            this.BeginAnimation(Window.LeftProperty, showAnimation);
        }
        public void MovedIn(object sender,EventArgs e) {
            mDanmakuManager.SetMoveLevelAvaliable(level);

            DoubleAnimation showAnimation = new DoubleAnimation();
            showAnimation.From = mDanmakuManager.SCREEN_WIDGH - this.ActualWidth;
            showAnimation.To = -this.ActualWidth;
            showAnimation.Duration = TimeSpan.FromSeconds(mDanmakuManager.SCREEN_WIDGH / (Width + mDanmakuManager.SCREEN_WIDGH)* DANMAKU_TIME);
            showAnimation.Completed += MovedOut;
            this.BeginAnimation(Window.LeftProperty, showAnimation);
        }
        public void MovedOut(object sender, EventArgs e) {                    
            this.Close();
        }
    }
}
