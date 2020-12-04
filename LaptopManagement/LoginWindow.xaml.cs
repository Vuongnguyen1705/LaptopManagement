using BLL;
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

namespace LaptopManagement
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        BLL_Login bLL_Login = new BLL_Login();
        BLL_Employee bLL_Employee = new BLL_Employee();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {
            if (bLL_Login.TryLogin(username.Text, password.Password) == 1)
            {
                UserSingleTon.Instance.User = bLL_Employee.getEmployeeByUsername(username.Text);
                new MainWindow().Show();
                Close();
            }
            else if (bLL_Login.TryLogin(username.Text, password.Password) == 2)
            {
                new MainWindow().Show();
                Close();
            }
            else
            {
                MessageBox.Show("Lỗi đăng nhập");
            }
        }
    }
}
