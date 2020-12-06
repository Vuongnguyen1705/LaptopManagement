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
        BLL_Login bLL_Login = new BLL_Login();
        BLL_Employee bLL_Employee = new BLL_Employee();
        private bool isLoading = true;
        public LoginWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
        }

        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {
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
                    if (role == 1)
                    {
                        UserSingleTon.Instance.User = bLL_Employee.getEmployeeByUsername(username);
                        new MainWindow().Show();
                        Close();
                    }
                    else if (bLL_Login.TryLogin(username, password) == 2)
                    {
                        UserSingleTon.Instance.User = bLL_Employee.getEmployeeByUsername(username);
                        new MainWindow().Show();
                        Close();
                    }
                    else
                    {
                        TextBlockError.Visibility = Visibility.Visible;
                        TextBlockError.Text = "Sai thông tin đăng nhập!";
                    }
                    ImageAwesomeLoading.Visibility = Visibility.Collapsed;
                }), DispatcherPriority.Background);
            }).Start();
        }

    }
}
