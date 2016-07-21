using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using ServerThreatment.Actions;
using Model.Structs;
using libDanmaku;
using libNetwork.Sockets;

namespace Main {
    /// <summary>
    /// MsgSender.xaml 的交互逻辑
    /// </summary>
    public partial class MsgSender : Window {
        const int HOTKEY_ID = 23333333;
        const float SCREEN_HEIGHT_PROPORTION = 0.8f;
        const float WINDOW_WIDTH_PROPORTION = 0.1f;
        readonly string UserName;
        Color DanmakuColor = Colors.White;
        Positions DanmakuPosition = Positions.Move;
        DanmakuManager mDanmakuManager;

        public MsgSender(string username) {
            InitializeComponent();
            this.Top = SystemParameters.WorkArea.Height * SCREEN_HEIGHT_PROPORTION;
            this.Left = SystemParameters.WorkArea.Width - this.Width * WINDOW_WIDTH_PROPORTION;
            UserName = username;

            Thread t = new Thread(ReceiveDanmaku);
            t.Start();
        }

        private void ReceiveDanmaku() {
            mDanmakuManager = new DanmakuManager();
            SockReceiver sr = new SockReceiver();
            var b = sr.ReceiveData();
            Message_mod mod = new Message_mod();
            mod.FromBytes(b);

            switch (mod.Position) {
                case Positions.Top:
                    mDanmakuManager.AddTopDanmaku(mod.StrMessage, mod.Sender, ConvertIntToColor(mod.Color));
                    break;
                case Positions.Move:
                    mDanmakuManager.AddMoveDanmaku(mod.StrMessage, mod.Sender, ConvertIntToColor(mod.Color));
                    break;
                case Positions.Bottom:
                    mDanmakuManager.AddBottomDanmaku(mod.StrMessage, mod.Sender, ConvertIntToColor(mod.Color));
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            IntPtr hWnd = new WindowInteropHelper(this).Handle;
            int fsKey = (int)HotKey.KeyModifiers.Ctrl | (int)HotKey.KeyModifiers.Alt;
            bool r = HotKey.RegisterHotKey(hWnd, HOTKEY_ID, fsKey, Key.Q);
            if (!r) {
                MessageBox.Show("热键注册失败惹~");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            IntPtr hWnd = new WindowInteropHelper(this).Handle;
            HotKey.UnRegHotKey(hWnd, HOTKEY_ID);
        }

        private void MouseEnter_(object sender, MouseEventArgs e) {
            DoubleAnimation animation = new DoubleAnimation();
            animation.DecelerationRatio = 0.5;
            animation.AccelerationRatio = 0.5;
            animation.From = this.Left;
            animation.To = SystemParameters.WorkArea.Width - this.Width;
            this.BeginAnimation(LeftProperty, animation);
        }

        private void LostFocus_(object sender, RoutedEventArgs e) {
            DoubleAnimation animation = new DoubleAnimation();
            animation.DecelerationRatio = 0.5;
            animation.AccelerationRatio = 0.5;
            animation.From = this.Left;
            animation.To = SystemParameters.WorkArea.Width - this.Width * WINDOW_WIDTH_PROPORTION;
            this.BeginAnimation(LeftProperty, animation);
        }

        private async void Button_Click(object sender, RoutedEventArgs e) {
            if(msgInput.Text != "") {
                await DCSendMessage.SendMessage(msgInput.Text, UserName, DanmakuPosition, ConvertColorToInt(DanmakuColor));
            }
        }
        private int ConvertColorToInt(Color c) {
            byte[] b = new byte[4];
            b[0] = c.A;
            b[1] = c.R;
            b[2] = c.G;
            b[3] = c.B;
            return BitConverter.ToInt32(b, 0);
        }
        private Color ConvertIntToColor(int value) {
            byte[] bytes = BitConverter.GetBytes(value);
            return Color.FromArgb(bytes[0], bytes[1], bytes[2], bytes[3]);
        }

        private void PackIcon_MouseUp(object sender, MouseButtonEventArgs e) {
            var s = new Settings(DanmakuColor, DanmakuPosition);
            s.Closing += (s_, e_) => {
                this.DanmakuColor = s.c;
                this.DanmakuPosition = s.p;
            };
            s.ShowDialog();
        }
    }
}
