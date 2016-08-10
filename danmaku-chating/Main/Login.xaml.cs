using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using ServerThreatment.Actions;
using ServerThreatment.Results;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Windows.Navigation;
using System.Windows.Media.Animation;
using libNetwork.Sockets;

namespace Main {
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window {
        private string UserName;
        private readonly Thickness CREATE_MARGIN = new Thickness(155, 222, 288, 0);
        private readonly Thickness JOIN_MARGIN = new Thickness(293, 222, 0, 0);
        private readonly Thickness HINT_MARGIN = new Thickness(87, 34, 0, 0);
        private readonly Thickness JOIN_ICON_MARGIN = new Thickness(259, 60, 0, 0);
        private readonly Thickness CREATE_ICON_MARGIN = new Thickness(124, 122, 0, 0);
        private readonly Thickness GO_BUTTON_MARGIN = new Thickness(184, 181, 184, 77);
        private readonly Thickness USERNAM＿INPUT_MARGIN = new Thickness(91, 79, 92, 172);
        private readonly Thickness INVAID_SHOW_MARGIN = new Thickness(91, 121, 0, 0);

        public Login() {
            InitializeComponent();
            ConnectToServer.Connect();
        }
        
        private void icons_MouseEnter(object sender, MouseEventArgs e) {
            ((Control)sender).Background = Brushes.LightGray;
        }
        private void icons_MouseLeave(object sender, MouseEventArgs e) {
            ((MaterialDesignThemes.Wpf.PackIcon)sender).Background = Brushes.Transparent;
        }
        private void icons_MouseDown(object sender, MouseButtonEventArgs e) {
            ((MaterialDesignThemes.Wpf.PackIcon)sender).Background = Brushes.Gray;
        }
        private void icons_MouseUp(object sender, MouseButtonEventArgs e) {
            PackIcon p = (MaterialDesignThemes.Wpf.PackIcon)sender;
            p.Background = Brushes.LightGray;
            switch (p.Name) {
                case "createIcon":
                    DoCreatAnimations();
                    break;
                case "joinIcon":
                    DoJoinAnimations();
                    break;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e) {
            if (usernameInput.Text != "" && uiHint.Text == "Username") {
                UserName = usernameInput.Text;
                DoAnimations();
            }
            if (usernameInput.Text != "" && uiHint.Text == "Group name") {
                goBtn.IsEnabled = false;
                var r = await DCLogin.DoLogin(UserName, usernameInput.Text);
                if (r.IsSucceed) {
                    new MsgSender(UserName).Show();
                } else {
                    MessageBox.Show("登录失败惹~");
                }
            }
            if (usernameInput.Text != "" && uiHint.Text == "Group id") {
                goBtn.IsEnabled = false;
                var r = await DCJoin.DoJoin(UserName, usernameInput.Text);
                if (r.IsSucceed) {
                    new MsgSender(UserName).Show();
                } else {
                    MessageBox.Show("登录失败惹~");
                }
            }
        }

        private void usernameInput_TextChanged(object sender, TextChangedEventArgs e) {
            invaidShow.Visibility = usernameInput.Text == "" ? Visibility.Visible : Visibility.Hidden;
        }

        private void DoAnimations() {
            invaidShow.Visibility = Visibility.Collapsed;

            ThicknessAnimation animation = new ThicknessAnimation();
            animation.AccelerationRatio = 0.5;
            animation.DecelerationRatio = 0.5;

            animation.To = new Thickness(-usernameInput.ActualWidth, usernameInput.Margin.Top, this.ActualWidth, usernameInput.Margin.Bottom);
            usernameInput.BeginAnimation(MarginProperty, animation);

            animation.To = new Thickness(-goBtn.ActualWidth, goBtn.Margin.Top, this.ActualWidth, goBtn.Margin.Bottom);
            goBtn.BeginAnimation(MarginProperty, animation);

            animation.AccelerationRatio = 0.5;
            animation.DecelerationRatio = 0.5;

            animation.To = HINT_MARGIN;
            hint.BeginAnimation(MarginProperty, animation);
            animation.To = JOIN_MARGIN;
            join.BeginAnimation(MarginProperty, animation);
            animation.To = JOIN_ICON_MARGIN;
            joinIcon.BeginAnimation(MarginProperty, animation);
            animation.To = CREATE_ICON_MARGIN;
            createIcon.BeginAnimation(MarginProperty, animation);
            animation.To = CREATE_MARGIN;
            create.BeginAnimation(MarginProperty, animation);

            //new MsgSender().Show();
        }

        private void DoCreatAnimations() {
            uiHint.Text = "Group name";
            invaidShow.Text = "Invaild group name";

            ThicknessAnimation animation = new ThicknessAnimation();
            animation.AccelerationRatio = 0.5;
            animation.DecelerationRatio = 0.5;

            animation.To = USERNAM＿INPUT_MARGIN;
            usernameInput.BeginAnimation(MarginProperty, animation);
            animation.To = GO_BUTTON_MARGIN;
            animation.Completed += (s, e) => { invaidShow.Visibility = Visibility.Visible; };
            goBtn.BeginAnimation(MarginProperty, animation);

            DoubleAnimation dAnimation = new DoubleAnimation();
            dAnimation.From = 1;
            dAnimation.To = 0;
            hint.BeginAnimation(OpacityProperty, dAnimation);
            create.BeginAnimation(OpacityProperty, dAnimation);
            join.BeginAnimation(OpacityProperty, dAnimation);
            createIcon.BeginAnimation(OpacityProperty, dAnimation);
            joinIcon.BeginAnimation(OpacityProperty, dAnimation);

            createIcon.IsEnabled = false;
            joinIcon.IsEnabled = false;
        }

        private void DoJoinAnimations() {
            uiHint.Text = "Group id";
            invaidShow.Text = "Invaild group id";

            ThicknessAnimation animation = new ThicknessAnimation();
            animation.AccelerationRatio = 0.5;
            animation.DecelerationRatio = 0.5;

            animation.To = USERNAM＿INPUT_MARGIN;
            usernameInput.BeginAnimation(MarginProperty, animation);
            animation.To = GO_BUTTON_MARGIN;
            animation.Completed += (s, e) => { invaidShow.Visibility = Visibility.Visible; };
            goBtn.BeginAnimation(MarginProperty, animation);

            DoubleAnimation dAnimation = new DoubleAnimation();
            dAnimation.From = 1;
            dAnimation.To = 0;
            hint.BeginAnimation(OpacityProperty, dAnimation);
            create.BeginAnimation(OpacityProperty, dAnimation);
            join.BeginAnimation(OpacityProperty, dAnimation);
            createIcon.BeginAnimation(OpacityProperty, dAnimation);
            joinIcon.BeginAnimation(OpacityProperty, dAnimation);

            createIcon.IsEnabled = false;
            joinIcon.IsEnabled = false;
        }

        private void usernameInput_KeyUp(object sender, KeyEventArgs e) {
            if(e.Key == Key.Enter) {
                Button_Click(null, null);
            }
        }
    }
}
