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
using libDanmaku;

namespace Test {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DanmakuManager dm;
        public MainWindow() {
            InitializeComponent();
            dm = new DanmakuManager();
            dm.AddTopDanmaku("2333", "6666", Color.FromArgb(255, 255, 255, 255));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dm.AddMoveDanmaku(moveInput.Text, "SB", Color.FromArgb(255, 255, 255, 255));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dm.AddTopDanmaku(topInput.Text, "SB", Color.FromArgb(255, 255, 255, 255));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            dm.AddBottomDanmaku(topInput.Text, "SB", Colors.Blue);
        }
    }
}
