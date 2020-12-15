using BLL;
using DTO;
using DTO.format;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using ToastNotifications.Messages;

namespace LaptopManagement.pages
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private BLL_User bLL_User = new BLL_User();
        private BLL_Role bLL_Role = new BLL_Role();
        int idUser = -1;
        private readonly ToastViewModel _vm;
        public UserPage()
        {
            InitializeComponent();
            _vm = new ToastViewModel();
            ShowUser();
            Filter();
            DataContext = this;
        }

        public void ShowUser()
        {
            ObservableCollection<UserFormat> list = new ObservableCollection<UserFormat>();
            foreach (var item in new ObservableCollection<User>(bLL_User.getAllUser()))
            {
                if (item.username != UserSingleTon.Instance.User.username)
                    list.Add(new UserFormat(item.ID, item.username, item.password, item.firstName + " " + item.lastName, bLL_User.getGender(item.gender), item.birthDate.ToShortDateString(), item.address, item.joinDate.ToShortDateString(), item.isDisable, bLL_Role.getRoleNameByID(item.Role_ID)));
            }
            DataGridUser.ItemsSource = list;
        }
        
        private void Filter()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DataGridUser.ItemsSource);
            view.Filter = UsernameFilter;
        }

        private bool UsernameFilter(object item)
        {
            if (String.IsNullOrEmpty(TextBoxSearch.Text))
                return true;
            else
                return ((item as UserFormat).username.IndexOf(TextBoxSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        } 

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                MessageBox.Show(item.Content.ToString());
            }
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridUser.ItemsSource).Refresh();
        }

        private void ButtonDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (idUser != -1)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xóa người dùng", MessageBoxButton.OKCancel);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    bLL_User.deleteUserByID(idUser);
                    //ShowUser();
                }
                else
                {
                    _vm.ShowInformation("Rảnh quá ba không xóa bấm chi");
                }
            }
            else
            {
                _vm.ShowWarning("Vui lòng chọn dữ liệu cần xóa");
            }
        }

        private void ToggleButton_Checked_Active(object sender, RoutedEventArgs e)
        {
            CheckBox button = sender as CheckBox;
            UserFormat user = button.DataContext as UserFormat;
            int id = user.ID;
            bLL_User.DisableUser(id);
            //_vm.ShowInformation("Đã khóa tài khoản");
        }

        private void ToggleButton_Unchecked_DeActive(object sender, RoutedEventArgs e)
        {
            CheckBox button = sender as CheckBox;
            UserFormat user = button.DataContext as UserFormat;
            int id = user.ID;
            bLL_User.EnableUser(id);
            _vm.ShowSuccess("Tài khoản đã mở khóa");
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUserPage());
        }

        private void DataGridUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            UserFormat user = item.DataContext as UserFormat;
            idUser = user.ID;
        }
    }
}
