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
using Model.Structs;
using System.Windows.Forms;

namespace Main {
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : Window {
        public Color c;
        public Positions p;
        public Settings(Color c, Positions p) {
            InitializeComponent();
            this.c = c;
            this.p = p;
            switch (p) {
                case Positions.Top:
                    top.IsChecked = true;
                    break;
                case Positions.Move:
                    move.IsChecked = true;
                    break;
                case Positions.Bottom:
                    move.IsChecked = true;
                    break;
            }
            colorPreview.Background = new SolidColorBrush(c);
            colorPreview.BorderBrush = Brushes.Black;
        }

        private void colorPreview_MouseUp(object sender, MouseButtonEventArgs e) {
            ColorDialog cd = new ColorDialog();
            cd.Color = System.Drawing.Color.FromArgb(c.R, c.G, c.B);
            if(cd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                c = Color.FromArgb(255, cd.Color.R, cd.Color.G, cd.Color.B);
            }
        }

        private void top_Click(object sender, RoutedEventArgs e) {
            p = Positions.Top;
        }

        private void move_Click(object sender, RoutedEventArgs e) {
            p = Positions.Move;
        }

        private void bottom_Click(object sender, RoutedEventArgs e) {
            p = Positions.Bottom;
        }
    }
}
