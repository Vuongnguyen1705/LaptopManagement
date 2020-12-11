using BLL;
using DTO;
using DTO.format;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LaptopManagement.pages
{
    /// <summary>
    /// Interaction logic for InfoUserPage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private BLL_User bLL_User = new BLL_User();
        private BLL_Role bLL_Role = new BLL_Role();
        private bool gender;
        private ToastViewModel _vm;
        
        public ProfilePage()
        {
            InitializeComponent();
            ShowInfo();
            _vm = new ToastViewModel();
        }

        private void ShowInfo()
        {
            UserSingleTon.Instance.User = bLL_User.getUserByUsername(UserSingleTon.Instance.User.username);
            TextBoxUserName.Text = UserSingleTon.Instance.User.username;
            TextBoxFirstName.Text = UserSingleTon.Instance.User.firstName;
            TextBoxLastName.Text = UserSingleTon.Instance.User.lastName;
            DatePickerBirthday.Text = UserSingleTon.Instance.User.birthDate.ToString();
            TextBoxAddress.Text = UserSingleTon.Instance.User.address;
            DatePickerJoinDate.Text = UserSingleTon.Instance.User.joinDate.ToString();
            if (UserSingleTon.Instance.User.gender == true)
            {
                RadioMale.IsChecked = true;
            }
            else
            {
                RadioFmale.IsChecked = true;
            }
            if (UserSingleTon.Instance.User.isDisable == true)
            {
                TextBlockActive.Text = "Đã khóa";
                TextBlockActive.Foreground = Brushes.Red;
            }
            else
            {
                TextBlockActive.Text = "Đang hoạt động";
                TextBlockActive.Foreground = Brushes.Green;
            }
            ComboBoxRole.Text = bLL_Role.getRoleNameByID(UserSingleTon.Instance.User.Role_ID);
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            
            new Thread(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    ImageAwesomeLoading.Visibility = Visibility.Visible;//hiển thị loading
                    if (CheckBoxChangePass.IsChecked==true)
                    {
                        if (PasswordBoxOldPass.Password.Equals(Utils.DecryptString(UserSingleTon.Instance.User.password,Utils.passEncode)))
                        {
                            if (PasswordBoxNewPass.Password.Equals(PasswordBoxNewPassConfirm.Password))
                            {
                                bLL_User.Update(new User(UserSingleTon.Instance.User.ID, UserSingleTon.Instance.User.username, Utils.EncryptString(PasswordBoxNewPass.Password,Utils.passEncode), TextBoxFirstName.Text, TextBoxLastName.Text, gender, DateTime.Parse(DatePickerBirthday.Text), TextBoxAddress.Text, DateTime.Parse(DatePickerJoinDate.Text), bLL_User.getValueByStatus(TextBlockActive.Text), UserSingleTon.Instance.User.Role_ID));
                                _vm.ShowSuccess("Cập nhật thành công");
                                ImageAwesomeLoading.Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                _vm.ShowError("Mật khẩu mới không khớp");
                                ImageAwesomeLoading.Visibility = Visibility.Hidden;
                            }
                        }
                        else
                        {
                            _vm.ShowError("Mật khẩu cũ không chính xác!");
                            ImageAwesomeLoading.Visibility = Visibility.Hidden;
                        }

                    }
                    else
                    {
                        bLL_User.Update(new User(UserSingleTon.Instance.User.ID, UserSingleTon.Instance.User.username, UserSingleTon.Instance.User.password, TextBoxFirstName.Text, TextBoxLastName.Text, gender, DateTime.Parse(DatePickerBirthday.Text), TextBoxAddress.Text, DateTime.Parse(DatePickerJoinDate.Text), bLL_User.getValueByStatus(TextBlockActive.Text), UserSingleTon.Instance.User.Role_ID));
                        _vm.ShowSuccess("Cập nhật thành công");
                        ImageAwesomeLoading.Visibility = Visibility.Hidden;
                    }
                }), DispatcherPriority.Loaded);
            }).Start();
            //new Thread(() =>
            //{                
            //    Dispatcher.BeginInvoke(new Action(() =>
            //    {
            //        _vm.ShowSuccess("Cập nhật thành công");                    
            //        ImageAwesomeLoading.Visibility = Visibility.Hidden;
            //    }), DispatcherPriority.Background);
            //}).Start();            
        }

        //private void ButtonChangeActive_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ButtonChangeActive.Content.Equals("Khóa"))
        //    {
        //        TextBlockActive.Text = "Đã khóa";
        //        TextBlockActive.Foreground = Brushes.Red;
        //        ButtonChangeActive.Content = "Mở khóa";
        //        ButtonChangeActive.Background = Brushes.Green;
        //    }
        //    else
        //    {
        //        TextBlockActive.Text = "Đang hoạt động";
        //        TextBlockActive.Foreground = Brushes.Green;
        //        ButtonChangeActive.Content = "Khóa";
        //        ButtonChangeActive.Background = Brushes.Red;
        //    }
        //}

        private void RadioMale_Checked(object sender, RoutedEventArgs e)
        {
            gender = true;
        }

        private void RadioFmale_Checked(object sender, RoutedEventArgs e)
        {
            gender = false;
        }

        private void CheckBoxChangePass_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBoxChangePass_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
