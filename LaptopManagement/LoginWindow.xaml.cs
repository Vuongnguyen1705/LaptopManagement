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
            int role = -1;
            new Thread(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    ImageAwesomeLoading.Visibility = Visibility.Visible;//hiển thị loading
                    TextBlockError.Visibility = Visibility.Collapsed;
                    role = bLL_Login.TryLogin(username.Text, password.Password);
                }), System.Windows.Threading.DispatcherPriority.Loaded);
            }).Start();
            new Thread(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    Thread.Sleep(2000);
                    //tryLogin là hàm đọc db nếu đọc được trả về Role, sai thì báo lỗi;            
                    if (role == 1)
                    {
                        UserSingleTon.Instance.User = bLL_Employee.getEmployeeByUsername(username.Text);
                        new MainWindow().Show();
                        Close();
                    }
                    else if (bLL_Login.TryLogin(username.Text, password.Password) == 2)
                    {
                        UserSingleTon.Instance.User = bLL_Employee.getEmployeeByUsername(username.Text);
                        new MainWindow().Show();
                        Close();
                    }
                    else
                    {
                        TextBlockError.Visibility = Visibility.Visible;
                        TextBlockError.Text = "Sai thông tin đăng nhập";
                    }
                    ImageAwesomeLoading.Visibility = Visibility.Collapsed;
                }), System.Windows.Threading.DispatcherPriority.Background);
            }).Start();

        }

    }
}
