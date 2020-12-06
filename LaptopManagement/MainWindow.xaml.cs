using BLL;
using LaptopManagement.pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LaptopManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadContent();
            Application.Current.MainWindow = this;
            LayoutContent.Content = new StatisticPage();
        }

        private void Button_Menu_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case "ButtonProduct":
                    LayoutContent.Content = new ProductPage();
                    break;
                case "ButtonUser":
                    LayoutContent.Content = new UserPage();
                    break;
                case "ButtonCombo":
                    LayoutContent.Content = new ComboPage();
                    break;
                case "ButtonSell":
                    LayoutContent.Content = new SellPage();
                    break;
                case "ButtonStatistic":
                    LayoutContent.Content = new StatisticPage();
                    break;
            }
        }

        private void Button_Logout_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }
        private void LoadContent()
        {
            string firstName = UserSingleTon.Instance.User.lastName;
            string lastName = UserSingleTon.Instance.User.lastName;
            string fullName = firstName + " " + lastName;
            TextBlockUserName.Text = fullName;
        }

        private void TextBlockUserName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LayoutContent.Content = new ProfilePage();
        }
    }
}
