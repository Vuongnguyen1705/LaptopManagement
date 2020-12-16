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
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private BLL_Product bLL_Product = new BLL_Product();
        private BLL_Catalog bLL_Catalog = new BLL_Catalog();
        private BLL_Brand bLL_Brand = new BLL_Brand();
        private List<int> listIDProduct = new List<int>();
        private ToastViewModel noti;        
        public ProductPage()
        {
            InitializeComponent();
            SetVisiable();
            noti = new ToastViewModel();
            
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
                    ShowProduct();
                }), DispatcherPriority.Loaded);

            }).Start();

        }

        public void ShowProduct()
        {
            new Thread(() =>
            {
                ObservableCollection<ProductFormat> list = new ObservableCollection<ProductFormat>();
                foreach (var item in new ObservableCollection<Product>(bLL_Product.getAllProduct()))
                {
                    list.Add(new ProductFormat(false,item.ID, item.Product_Name, bLL_Catalog.getCatalogNameByID(item.Catalog_ID), item.Amount, item.Price, item.Image, (int)item.Discount, item.Detail, bLL_Brand.getBrandNameByID((int)item.Brand_ID)));
                }
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    ImageAwesomeLoading.Visibility = Visibility.Collapsed;
                    GridRoot.Visibility = Visibility.Visible;
                    DataGridProduct.ItemsSource = list;
                    Filter();
                }), DispatcherPriority.Background);
            }).Start();
            
        }

        private void Filter()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DataGridProduct.ItemsSource);
            view.Filter = ProductFilter;
        }

        private bool ProductFilter(object item)
        {
            if (String.IsNullOrEmpty(TextBoxSearch.Text))
                return true;
            else
                return ((item as ProductFormat).Product_Name.IndexOf(TextBoxSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        { 
            CollectionViewSource.GetDefaultView(DataGridProduct.ItemsSource).Refresh();            
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductPage());
        }

        private void DataGridProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductFormat product = (ProductFormat)DataGridProduct.SelectedItem;
            //MessageBox.Show(product.Product_Name.ToString());
            NavigationService.Navigate(new EditProductPage(product.Product_Name.ToString()));
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
            CheckBox check = sender as CheckBox;
            ProductFormat format = check.DataContext as ProductFormat;
            listIDProduct.Add(format.ID);
            format.isCheck = true;
            //noti.ShowInformation(format.ID.ToString());

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox check = sender as CheckBox;
            ProductFormat format = check.DataContext as ProductFormat;
            listIDProduct.Remove(format.ID);
            format.isCheck = false;
            //noti.ShowInformation(format.ID.ToString());
            
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (listIDProduct.Count != 0)
            {           
                MessageBoxResult messageBoxResult = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xóa sản phẩm", MessageBoxButton.OKCancel);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    foreach (var item in listIDProduct)
                    {
                        bLL_Product.deleteProductByID(item);
                    }
                    noti.ShowSuccess("Xóa thành công");
                    ShowProduct();
                }
            }
            else
            {
                noti.ShowWarning("Vui lòng chọn sản phẩm để xóa");
            }
            
        }
    }
}
