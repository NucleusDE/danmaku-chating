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
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Danmaku {
    /// <summary>
    /// DanmakuWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DanmakuWindow : Window {
        public readonly string DANMAKU_TEXT;
        public DanmakuManager mDanmakuManager { get; set; }
        public string sender { set; get; }
        private const int WsExTransparent = 0x20;
        private const int GwlExstyle = -20;
        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint dwNewLong);
        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

        public DanmakuWindow(string text, DanmakuManager pDanmakuManager, string sender, Color color) {
            InitializeComponent();
            DANMAKU_TEXT = text;
            mDanmakuManager = pDanmakuManager;
            this.sender = sender;
            DanmakuShow.Foreground = new SolidColorBrush(color);
            SourceInitialized += delegate
            {
                IntPtr hwnd = new WindowInteropHelper(this).Handle;
                uint extendedStyle = GetWindowLong(hwnd, GwlExstyle);
                SetWindowLong(hwnd, GwlExstyle, extendedStyle | WsExTransparent);
            };
        }

        internal void Window_Loaded(object sender, RoutedEventArgs e) {
            DanmakuShow.Text = DANMAKU_TEXT;
            this.Width = DanmakuShow.ActualWidth;
            DanmakuShow.Width = DanmakuShow.ActualWidth;
        }
    }
}
