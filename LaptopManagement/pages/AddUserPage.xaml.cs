using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace LaptopManagement.pages
{
    /// <summary>
    /// Interaction logic for AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        ToastViewModel noti = new ToastViewModel();
        private BLL_User bLL_User = new BLL_User();
        private BLL_Role bLL_Role = new BLL_Role();
        private bool gender;
        private ToastViewModel _vm;
        private bool flagFistName = false, flagLastName = false, flagAddress = false, flagConfirmPass = false, flagOldPass = false, flagCheckChangePass = false;
        public AddUserPage()
        {
            InitializeComponent();
            _vm = new ToastViewModel();
        }

        private void RadioMale_Checked(object sender, RoutedEventArgs e)
        {
            gender = true;
        }

        private void RadioFmale_Checked(object sender, RoutedEventArgs e)
        {
            gender = false;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

            if (flagFistName == true && flagLastName == true && flagAddress == true)
            {
                bool flag = true;
                foreach (var s in bLL_User.getAllUser())
                {
                    if (s.username.Equals(TextBoxUserName.Text))
                    {
                        flag = false;
                        break;
                    }
                }
                if(flag==true)
                {
                    bLL_User.AddUser(new User(10, TextBoxUserName.Text, Utils.EncryptString(PasswordBox.Password, Utils.passEncode), TextBoxFirstName.Text, TextBoxLastName.Text, gender, DateTime.Parse(DatePickerBirthday.Text), TextBoxAddress.Text, DateTime.Parse(DatePickerJoinDate.Text), false, bLL_Role.getIDByRoleName(ComboBoxRole.Text)));
                    _vm.ShowSuccess("Thêm thành công");
                }
                else
                {
                    noti.ShowError("Tài khoản đã được sử dụng.");
                }
            }
            else
            {
                _vm.ShowError("Vui lòng nhập đúng các thông tin");
            }



            //if (TextBoxUserName.Text == "" || PasswordBox.Password == "" || PasswordBoxConfirm.Password == "" || TextBoxFirstName.Text == "" || TextBoxLastName.Text == "" || TextBoxAddress.Text == "")
            //{
            //    noti.ShowError("Vui lòng nhập đầy đủ thông tin.");
            //}
            //else
            //{
            //    if (!PasswordBox.Password.Equals(PasswordBoxConfirm.Password))
            //    {
            //        noti.ShowError("Mật khẩu không khớp, vui lòng nhập lại");                
            //    }
            //    else
            //    {
            //        bool flag = true;
            //        foreach (var s in bLL_User.getAllUser())
            //        {
            //            if (s.username.Equals(TextBoxUserName.Text))
            //            {
            //                noti.ShowError("Tài khoản đã được sử dụng.");
            //                flag = false;
            //                break;
            //            }
            //        }
            //        if (flag == true)
            //        {
            //            if (PasswordBox.Password.Equals(PasswordBoxConfirm.Password))
            //            {
            //                bLL_User.AddUser(new User(10, TextBoxUserName.Text, Utils.EncryptString(PasswordBox.Password, Utils.passEncode), TextBoxFirstName.Text, TextBoxLastName.Text, gender, DateTime.Parse(DatePickerBirthday.Text), TextBoxAddress.Text, DateTime.Parse(DatePickerJoinDate.Text), false, bLL_Role.getIDByRoleName(ComboBoxRole.Text)));
            //            noti.ShowSuccess("Thêm user thành công.");
            //            }
            //            else
            //            {
            //                _vm.ShowError("Mật khẩu không khớp, vui lòng kiểm tra lại");
            //            }
            //        }
            //    }
            //}

        }

        private void TextBoxAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                TextBlockAddressError.Visibility = Visibility.Visible;
                TextBlockAddressError.Text = "Không được để trống";
                flagAddress = false;
            }
            else
            {
                if (!Regex.IsMatch(textBox.Text, @"^[a-z0-9A-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\s\W]+$"))
                {
                    TextBlockAddressError.Visibility = Visibility.Visible;
                    TextBlockAddressError.Text = "Giá trị không hợp lệ";
                    flagAddress = false;
                }
                else
                {
                    TextBlockAddressError.Visibility = Visibility.Collapsed;
                    flagAddress = true;
                }
            }
        }

        private void TextBoxUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                TextBlockUserNameError.Visibility = Visibility.Visible;
                TextBlockUserNameError.Text = "Không được để trống";
                flagFistName = false;
            }
            else
            {
                if (!Regex.IsMatch(textBox.Text, @"^[a-zA-Z0-9]+$"))
                {
                    TextBlockUserNameError.Visibility = Visibility.Visible;
                    TextBlockUserNameError.Text = "Giá trị không hợp lệ";
                    flagFistName = false;
                }
                else
                {
                    TextBlockUserNameError.Visibility = Visibility.Collapsed;
                    flagFistName = true;
                }
            }
        }

        private void TextBoxLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                TextBlockLastNameError.Visibility = Visibility.Visible;
                TextBlockLastNameError.Text = "Không được để trống";
                flagLastName = false;
            }
            else
            {
                if (!Regex.IsMatch(textBox.Text, @"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\s_]+$"))
                {
                    TextBlockLastNameError.Visibility = Visibility.Visible;
                    TextBlockLastNameError.Text = "Giá trị không hợp lệ";
                    flagLastName = false;
                }
                else
                {
                    TextBlockLastNameError.Visibility = Visibility.Collapsed;
                    flagLastName = true;
                }
            }
        }

        private void TextBoxFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                TextBlockFirstNameError.Visibility = Visibility.Visible;
                TextBlockFirstNameError.Text = "Không được để trống";
                flagFistName = false;
            }
            else
            {
                if (!Regex.IsMatch(textBox.Text, @"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\s_]+$"))
                {
                    TextBlockFirstNameError.Visibility = Visibility.Visible;
                    TextBlockFirstNameError.Text = "Giá trị không hợp lệ";
                    flagFistName = false;
                }
                else
                {
                    TextBlockFirstNameError.Visibility = Visibility.Collapsed;
                    flagFistName = true;
                }
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password.Length < 6)
            {
                TextBlockNewPassConfirmError.Visibility = Visibility.Visible;
                TextBlockNewPassConfirmError.Text = "Mật khẩu phải từ 6 ký tự trở lên";
                TextBlockNewPassConfirmError.Foreground = Brushes.Red;
                flagConfirmPass = false;
            }
            else
            {
                if (PasswordBox.Password.Equals(PasswordBoxConfirm.Password))
                {
                    TextBlockNewPassConfirmError.Visibility = Visibility.Visible;
                    TextBlockNewPassConfirmError.Text = "✔";
                    TextBlockNewPassConfirmError.Foreground = Brushes.Green;
                    flagConfirmPass = true;
                }
                else
                {
                    TextBlockNewPassConfirmError.Visibility = Visibility.Visible;
                    TextBlockNewPassConfirmError.Text = "Mật khẩu không khớp";
                    TextBlockNewPassConfirmError.Foreground = Brushes.Red;
                    flagConfirmPass = false;
                }
            }
        }

        private void PasswordBoxConfirm_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password.Length < 6)
            {
                TextBlockNewPassConfirmError.Visibility = Visibility.Visible;
                TextBlockNewPassConfirmError.Text = "Mật khẩu phải từ 6 ký tự trở lên";
                TextBlockNewPassConfirmError.Foreground = Brushes.Red;
                flagConfirmPass = false;
            }
            else
            {
                if (PasswordBox.Password.Equals(PasswordBoxConfirm.Password))
                {
                    TextBlockNewPassConfirmError.Visibility = Visibility.Visible;
                    TextBlockNewPassConfirmError.Text = "✔";
                    TextBlockNewPassConfirmError.Foreground = Brushes.Green;
                    flagConfirmPass = true;
                }
                else
                {
                    TextBlockNewPassConfirmError.Visibility = Visibility.Visible;
                    TextBlockNewPassConfirmError.Text = "Mật khẩu không khớp";
                    TextBlockNewPassConfirmError.Foreground = Brushes.Red;
                    flagConfirmPass = false;
                }
            }
        }
    }
}
