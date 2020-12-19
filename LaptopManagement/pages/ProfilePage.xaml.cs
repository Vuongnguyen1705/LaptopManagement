using BLL;
using DTO;
using DTO.format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private bool flagFistName = false, flagLastName = false, flagAddress = false, flagConfirmPass = false, flagOldPass = false, flagCheckChangePass = false;
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

            TextBlockActive.Text = "Đang hoạt động";
            TextBlockActive.Foreground = Brushes.Green;
            ComboBoxRole.Text = bLL_Role.getRoleNameByID(UserSingleTon.Instance.User.Role_ID);
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

            new Thread(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    ImageAwesomeLoading.Visibility = Visibility.Visible;//hiển thị loading

                    if (flagCheckChangePass == true)
                    {
                        if (flagConfirmPass == true && flagOldPass == true)
                        {
                            if (flagFistName == true && flagLastName == true && flagAddress == true)
                            {
                                bLL_User.Update(new User(UserSingleTon.Instance.User.ID, UserSingleTon.Instance.User.username, Utils.EncryptString(PasswordBoxNewPass.Password, Utils.passEncode), TextBoxFirstName.Text, TextBoxLastName.Text, gender, DateTime.Parse(DatePickerBirthday.Text), TextBoxAddress.Text, DateTime.Parse(DatePickerJoinDate.Text), bLL_User.getValueByStatus(TextBlockActive.Text), UserSingleTon.Instance.User.Role_ID));
                                _vm.ShowSuccess("Cập nhật thành công");
                            }
                            else
                            {
                                _vm.ShowError("Vui lòng nhập đúng các thông tin");
                            }
                        }
                        else
                        {
                            _vm.ShowError("Mật khẩu cũ không đúng");
                        }
                        ImageAwesomeLoading.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        if (flagFistName == true && flagLastName == true && flagAddress == true)
                        {
                            bLL_User.Update(new User(UserSingleTon.Instance.User.ID, UserSingleTon.Instance.User.username, UserSingleTon.Instance.User.password, TextBoxFirstName.Text, TextBoxLastName.Text, gender, DateTime.Parse(DatePickerBirthday.Text), TextBoxAddress.Text, DateTime.Parse(DatePickerJoinDate.Text), bLL_User.getValueByStatus(TextBlockActive.Text), UserSingleTon.Instance.User.Role_ID));
                            _vm.ShowSuccess("Cập nhật thành công");
                            ImageAwesomeLoading.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            _vm.ShowError("Vui lòng nhập đúng các thông tin");
                            ImageAwesomeLoading.Visibility = Visibility.Collapsed;
                        }
                    }
                }), DispatcherPriority.Loaded);
            }).Start();
        }

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
            flagCheckChangePass = false;
        }

        private void CheckBoxChangePass_Checked(object sender, RoutedEventArgs e)
        {
            flagCheckChangePass = true;
        }

        private void PasswordBoxNewPassConfirm_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBoxNewPass.Password.Length < 6)
            {
                TextBlockNewPassConfirmError.Visibility = Visibility.Visible;
                TextBlockNewPassConfirmError.Text = "Mật khẩu phải từ 6 ký tự trở lên";
                TextBlockNewPassConfirmError.Foreground = Brushes.Red;
                flagConfirmPass = false;
            }
            else
            {
                if (PasswordBoxNewPassConfirm.Password.Equals(PasswordBoxNewPass.Password))
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

        private void PasswordBoxNewPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBoxNewPass.Password.Length < 6)
            {
                TextBlockNewPassConfirmError.Visibility = Visibility.Visible;
                TextBlockNewPassConfirmError.Text = "Mật khẩu phải từ 6 ký tự trở lên";
                TextBlockNewPassConfirmError.Foreground = Brushes.Red;
                flagConfirmPass = false;
            }
            else
            {
                if (PasswordBoxOldPass.Password.Equals(PasswordBoxNewPassConfirm.Password))
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

        private void PasswordBoxOldPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!PasswordBoxOldPass.Password.Equals(Utils.DecryptString(UserSingleTon.Instance.User.password, Utils.passEncode)))
            {
                TextBlockOldPassError.Visibility = Visibility.Visible;
                TextBlockOldPassError.Text = "✘";
                TextBlockOldPassError.Foreground = Brushes.Red;
                flagOldPass = false;
            }
            else
            {
                TextBlockOldPassError.Visibility = Visibility.Visible;
                TextBlockOldPassError.Text = "✔";
                TextBlockOldPassError.Foreground = Brushes.Green;
                flagOldPass = true;
            }
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

    }
}
