using BLL;
using DTO;
using DTO.format;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        BLL_Order bLL_Order = new BLL_Order();
        BLL_User bLL_User = new BLL_User();
        BLL_Product bLL_Product = new BLL_Product();
        BLL_Combo bLL_Combo = new BLL_Combo();
        BLL_OrderDetail bLL_OrderDetail = new BLL_OrderDetail();
        ObservableCollection<ProductSelectionFormat> listProductSelection = new ObservableCollection<ProductSelectionFormat>();
        ObservableCollection<OrderDetailFormat> listSelected = new ObservableCollection<OrderDetailFormat>();
        ProductSelectionFormat formatSelection;
        ToastViewModel noti = new ToastViewModel();
        private double sumTotalMoney = 0;
        Dictionary<int, string> list = new Dictionary<int, string>();
        public OrderPage()
        {

            InitializeComponent();
            SetVisiable();
            ShowProductSelection();
            PSFilter();
            ShowCustomerName();


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
                    ShowOrder();
                }), DispatcherPriority.Loaded);

            }).Start();

        }

        public void ShowOrder()
        {
            new Thread(() =>
            {
                string status = "";

                ObservableCollection<OrderFormat> list = new ObservableCollection<OrderFormat>();
                foreach (var item in bLL_Order.getAllOrder())
                {
                    var user = bLL_User.getUserByID((int)item.Customer_Id);
                    var nameuser = user.firstName + " " + user.lastName;
                    switch (item.Status)
                    {
                        case 1:
                            status = "Đã tiếp nhận";
                            break;
                        case 2:
                            status = "Đang xử lý";
                            break;
                        case 3:
                            status = "Đã hoàn thành";
                            break;
                        case 4:
                            status = "Đã hủy";
                            break;
                    }
                    list.Add(new OrderFormat(item.ID, user.ID, (DateTime)item.Date, (double)item.Total_Price, nameuser, status));
                }
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    ImageAwesomeLoading.Visibility = Visibility.Collapsed;
                    GridRoot.Visibility = Visibility.Visible;
                    DataGridOrder.ItemsSource = list;
                    Filter();
                }), DispatcherPriority.Background);
            }).Start();
        }

        private void Filter()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DataGridOrder.ItemsSource);
            view.Filter = OrderFilter;
        }

        private bool OrderFilter(object item)
        {
            if (String.IsNullOrEmpty(TextBoxSearch.Text))
                return true;
            else
                return ((item as OrderFormat).ID.ToString().IndexOf(TextBoxSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridOrder.ItemsSource).Refresh();
        }



        public void ShowProductSelection()
        {
            foreach (var item in bLL_Product.getAllProduct())
            {
                // var user = bLL_User.getUserByID((int)item.Customer_Id);
                listProductSelection.Add(new ProductSelectionFormat(item.ID, item.Product_Name, Convert.ToDouble(item.Price), "Sản phẩm"));
            }
            foreach (var item in bLL_Combo.getAllCombo())
            {
                // var user = bLL_User.getUserByID((int)item.Customer_Id);
                listProductSelection.Add(new ProductSelectionFormat(item.ID, item.Combo_Name, Convert.ToDouble((item.totalMoney - (item.totalMoney * item.discount / 100)).ToString("#,##0.00")), "Combo"));
            }
            DataGridProductSelection.ItemsSource = listProductSelection;
        }

        public void ShowOrderDetail()
        {
            DataGridProductSelection.ItemsSource = listSelected;
        }





        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxQuantity.Text == "")
            {
                noti.ShowError("Vui lòng nhập số lượng.");
            }
            else
            {
                if (formatSelection == null)
                {
                    noti.ShowError("Vui lòng chọn sản phẩm");
                }
                else
                {
                    listSelected.Add(new OrderDetailFormat(formatSelection.ID, formatSelection.Name, formatSelection.Type, formatSelection.Price * Convert.ToInt32(TextBoxQuantity.Text), Convert.ToInt32(TextBoxQuantity.Text)));
                    DataGridOrderDetail.ItemsSource = listSelected;
                    //CollectionViewSource.GetDefaultView(DataGridOrderDetail.ItemsSource).Refresh();           
                }
            }
        }

        

        private void DataGridProductSelection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ProductSelectionFormat format = DataGridProductSelection.SelectedItem as ProductSelectionFormat;
            formatSelection = format;
            //MessageBox.Show(format.Name + "--" + formatSelection.Name);
        }





        

        private void DataGridOrder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            listSelected.Clear();
            OrderFormat order = DataGridOrder.SelectedItem as OrderFormat;
            if (order != null)
            {
                ObservableCollection<OrderDetail> OD = bLL_OrderDetail.getAllODByOrderID(order.ID);

                if (!order.Status.Equals("Đã tiếp nhận"))  //disable các text box nếu status khác 1
                {
                    TextBoxQuantity.IsEnabled = false;
                    TextBoxSearchProduct.IsEnabled = false;
                    ButtonAddOrderDetail.IsEnabled = false;
                    ButtonSaveOrderDetail.IsEnabled = false;
                    ButtonDeleteOrderDetail.IsEnabled = false;
                    DataGridProductSelection.IsEnabled = false;
                    foreach (var item in OD)
                    {
                        if (item.Product_Id == 0)
                        {
                            if (item.Price != null)
                                listSelected.Add(new OrderDetailFormat(1, bLL_Combo.getComboNameByID((int)item.Combo_Id), "Combo", (double)(item.Price * item.Quantity), (int)item.Quantity)); //làm hàm get comboname
                        }
                        else
                        {
                            if (item.Price != null)
                                listSelected.Add(new OrderDetailFormat(1, bLL_Product.getProductNameByid((int)item.Product_Id), "Sản phẩm", (double)(item.Price * item.Quantity), (int)item.Quantity));
                        }
                    }
                    DataGridOrderDetail.ItemsSource = listSelected;
                }
                else
                {
                    TextBoxQuantity.IsEnabled = true;
                    TextBoxSearchProduct.IsEnabled = true;
                    ButtonAddOrderDetail.IsEnabled = true;
                    ButtonSaveOrderDetail.IsEnabled = true;
                    ButtonDeleteOrderDetail.IsEnabled = true;
                    DataGridProductSelection.IsEnabled = true;
                    foreach (var item in OD)
                    {
                        if (item.Product_Id == 0)
                        {
                            if (item.Price != null)
                                listSelected.Add(new OrderDetailFormat(1, bLL_Combo.getComboNameByID((int)item.Combo_Id), "Combo", (double)(item.Price * item.Quantity), (int)item.Quantity)); //làm hàm get comboname
                        }
                        else
                        {
                            if (item.Price != null)
                                listSelected.Add(new OrderDetailFormat(1, bLL_Product.getProductNameByid((int)item.Product_Id), "Sản phẩm", (double)(item.Price * item.Quantity), (int)item.Quantity));
                        }
                    }
                    DataGridOrderDetail.ItemsSource = listSelected;
                }
            }

        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (e.Handled != regex.IsMatch(e.Text))
            {
                e.Handled = regex.IsMatch(e.Text);
                noti.ShowError("Vui lòng nhập số vào ô này");
            }

        }

        private void ButtonSaveOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            OrderFormat order = DataGridOrder.SelectedItem as OrderFormat;

            foreach (var item in listSelected)
            {
                if (item.OD_Type == "Sản phẩm")
                {
                    //MessageBox.Show(item.OD_TotalMoney + "");
                    bLL_OrderDetail.AddOrderDetail(new OrderDetail(1, order.ID, bLL_Product.getProductIDByName(item.OD_Name), 0, item.OD_Quantity, item.OD_TotalMoney));

                }
                else
                {
                    bLL_OrderDetail.AddOrderDetail(new OrderDetail(1, order.ID, 0, bLL_Combo.getComboIDByName(item.OD_Name), item.OD_Quantity, item.OD_TotalMoney));
                }
            }

            foreach (var item in listSelected)
            {
                sumTotalMoney += item.OD_TotalMoney;
            }
            int statusInt = 0;
            switch (order.Status)
            {
                case "Đã tiếp nhận":
                    statusInt = 1;
                    break;
                case "Đang xử lý":
                    statusInt = 2;
                    break;
                case "Đã hoàn thành":
                    statusInt = 3;
                    break;
                case "Đã hủy":
                    statusInt = 4;
                    break;
            }
            bLL_Order.UpdateOrder(new Order(order.ID, order.Date, sumTotalMoney, 1, statusInt));
            ShowOrder();
        }

        private void ButtonDeleteOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Bạn chắc chắn muốn xóa tất cả?", "Xóa sản phẩm", MessageBoxButton.OKCancel);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                listSelected.Clear();
                DataGridOrderDetail.ItemsSource = null;
            }
        }


  
        private void ComboBoxStatus_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;

            OrderFormat order = combo.DataContext as OrderFormat;
            /*int statusInt = 0;
            switch (order.Status)
            {
                case "Đã tiếp nhận":
                    statusInt = 1;
                    break;
                case "Đang xử lý":
                    statusInt = 2;
                    break;
                case "Đã hoàn thành":
                    statusInt = 3;
                    break;
                case "Đã hủy":
                    statusInt = 4;
                    break;
            }*/
            //MessageBox.Show((combo.SelectedIndex + 1 )+ "");
            bLL_Order.UpdateOrder(new Order(order.ID, order.Date, order.Total_Price, order.Customer_Id, combo.SelectedIndex+1));
            ShowOrder();
        }


        private void PSFilter()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DataGridProductSelection.ItemsSource);
            view.Filter = ProductSelectionFilter;
        }

        private bool ProductSelectionFilter(object item)
        {
            if (String.IsNullOrEmpty(TextBoxSearchProduct.Text))
                return true;
            else
                return ((item as ProductSelectionFormat).Name.IndexOf(TextBoxSearchProduct.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void TextBoxSearchProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridProductSelection.ItemsSource).Refresh();
        }

        private void ButtonDeleteSelectedOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            /*foreach (var item in listSelected)
            listSelected.Remove();*/
        }
        private void ShowCustomerName()
        {
          
            foreach(var item in bLL_User.getAllUser())
            {
                list.Add(item.ID,item.firstName + " " + item.lastName);
            }
            ComboBoxCustomer.ItemsSource = list.Values;
            ComboBoxCustomer.SelectedIndex = 1;
        }
        private void ButtonAddOrder_Click(object sender, RoutedEventArgs e)
        {
            int _id = 0;
            foreach (var item in list)
            {
                if (item.Value.Equals(ComboBoxCustomer.SelectedItem))
                {
                    //MessageBox.Show(ComboBoxCustomer.SelectedItem + "");
                    _id = item.Key;
                    break;
                }
            }
            bLL_Order.AddOrder(new Order(1, DateTime.Today, 0, _id, 1));
            ShowOrder();

        }
        private void ComboBoxCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

/*Error list:
 * Chưa làm được user nên set cứng là 1*/

