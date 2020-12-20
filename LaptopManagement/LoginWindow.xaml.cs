using BLL;
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
using System.Windows.Threading;

namespace LaptopManagement
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private BLL_Login bLL_Login = new BLL_Login();
        private BLL_User bLL_Employee = new BLL_User();
        private ToastViewModel _vm;        
        public LoginWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
            _vm = new ToastViewModel();
        }

        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {
            ButtonLogin.IsEnabled = false;
            string username = TextBoxUserName.Text;
            string password = PasswordBoxPassword.Password;
            new Thread(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    ImageAwesomeLoading.Visibility = Visibility.Visible;//hiển thị loading
                    TextBlockError.Visibility = Visibility.Collapsed;
                }), DispatcherPriority.Loaded);
            }).Start();
            new Thread(() =>
            {
                int role = bLL_Login.TryLogin(username, password);
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    switch (role)
                    {
                        case -1:
                            TextBlockError.Visibility = Visibility.Visible;
                            _vm.ShowError("Tài khoản của bạn bị khóa! Vui lòng liên hệ admin");
                            ButtonLogin.IsEnabled = true;
                            break;
                        case 1:
                            UserSingleTon.Instance.User = bLL_Employee.getUserByUsername(username);
                            new MainWindow(1).Show();
                            Close();
                            break;
                        case 2:
                            UserSingleTon.Instance.User = bLL_Employee.getUserByUsername(username);
                            new MainWindow(2).Show();
                            Close();
                            break;
                        case 3:
                            TextBlockError.Visibility = Visibility.Visible;
                            TextBlockError.Text = "Bạn không đủ quyền đăng nhập ";
                            ButtonLogin.IsEnabled = true;
                            break;
                        default:
                            TextBlockError.Visibility = Visibility.Visible;
                            TextBlockError.Text = "Sai thông tin đăng nhập!";
                            ButtonLogin.IsEnabled = true;
                            break;
                    }                    
                    ImageAwesomeLoading.Visibility = Visibility.Collapsed;
                }), DispatcherPriority.Background);
            }).Start();
        }

    }
}
