using BLL;
using DTO;
using DTO.format;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ComboPage.xaml
    /// </summary>
    public partial class ComboPage : Page
    {
        private BLL_Combo bLL_Combo;
        private BLL_Product bLL_Product;
        private readonly ToastViewModel _vm;
        private List<int> listIDCombo = new List<int>();
        public ComboPage()
        {
            InitializeComponent();
            _vm = new ToastViewModel();
            bLL_Combo = new BLL_Combo();
            bLL_Product = new BLL_Product();
            SetVisiable();
            DataContext = this;
        }

        private void SetVisiable()
        {
            new Thread(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    GridRoot.Visibility = Visibility.Collapsed;
                    ImageAwesomeLoading.Visibility = Visibility.Visible;//hiển thị loading
                }), DispatcherPriority.Loaded);
            }).Start();

            new Thread(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    ShowCombo();
                }), DispatcherPriority.Loaded);

            }).Start();
        }

        public void ShowCombo()
        {
            new Thread(() =>
            {
                ObservableCollection<ComboFormat> list = new ObservableCollection<ComboFormat>();
                string[] temp_productList;
                string products;
                foreach (var item in new ObservableCollection<Combo>(bLL_Combo.getAllCombo()))
                {
                    products = "";
                    temp_productList = item.Product_List.Split(';');
                    foreach (var i in temp_productList)
                    {
                        products += bLL_Product.getProductNameByid(int.Parse(i)) + "\n";
                    }
                    list.Add(new ComboFormat(item.ID, item.Combo_Name, products.Trim(), item.startDate.ToShortDateString(), item.endDate.ToShortDateString(), item.totalMoney, item.discount));
                }
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    ImageAwesomeLoading.Visibility = Visibility.Collapsed;
                    GridRoot.Visibility = Visibility.Visible;
                    DataGridCombo.ItemsSource = list;
                    Filter();
                }), DispatcherPriority.Background);
            }).Start();

        }

        private void Filter()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DataGridCombo.ItemsSource);
            view.Filter = ComboNameFilter;
        }

        private bool ComboNameFilter(object item)
        {
            if (String.IsNullOrEmpty(TextBoxSearch.Text))
                return true;
            else
                return ((item as ComboFormat).Combo_Name.IndexOf(TextBoxSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridCombo.ItemsSource).Refresh();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listIDCombo.Count != 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xóa sản phẩm", MessageBoxButton.OKCancel);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    foreach (var id in listIDCombo)
                    {
                        bLL_Combo.Delete(id);
                    }
                    _vm.ShowSuccess("Xóa thành công");
                }
            }
            else
            {
                _vm.ShowWarning("Vui lòng chọn Combo để xóa");
            }
        }

        private void ChecboxDelete_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox check = sender as CheckBox;
            ComboFormat comboFormat = check.DataContext as ComboFormat;
            listIDCombo.Add(comboFormat.ID);
        }

        private void ChecboxDelete_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox check = sender as CheckBox;
            ComboFormat comboFormat = check.DataContext as ComboFormat;
            _vm.ShowError(comboFormat.ID.ToString());
        }

    }
}
