using DTO;
using BLL;
using Microsoft.Win32;
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
    /// Interaction logic for AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        private BLL_Catalog bLL_Catalog = new BLL_Catalog();
        private BLL_Brand bLL_Brand = new BLL_Brand();
        private BLL_Product bLL_Product = new BLL_Product();
        ToastViewModel noti = new ToastViewModel();
        public AddProductPage()
        {

            InitializeComponent();
            ShowProductCatalog();
            ShowProductBrand();
        }

        private void ShowProductCatalog()
        {           
            ComboBoxCatalog.ItemsSource= bLL_Catalog.getAllCatalog();       
        }
        private void ShowProductBrand()
        {
            ComboBoxBrand.ItemsSource = bLL_Brand.getAllBrand();
        }


        private void TextBoxProductName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ImageBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {

                Uri fileUri = new Uri(openFileDialog.FileName);
                ImageBox.Source = new BitmapImage(fileUri);
                string filePath = fileUri.ToString().Remove(0, 8);
                string destinationDir = "..\\..\\images\\Products\\";
                System.IO.File.Copy(filePath, destinationDir + System.IO.Path.GetFileName(filePath), true);
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

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxProductName.Text == "" || TextAreaDetail.Text == "" || ComboBoxCatalog.SelectedIndex == -1 || ComboBoxBrand.SelectedIndex == -1 || TextBoxAmount.Text == "" || TextBoxDiscount.Text == "")
            {
                noti.ShowError("Vui lòng nhập đầy đủ thông tin.");
            }
            else
            {
                if (Convert.ToInt32(TextBoxDiscount.Text) > 100)
                {
                    noti.ShowError("Tỉ lệ giảm giá không được lớn hơn 100%");
                }
                else 
                { 
                    bLL_Product.AddProduct(new Product(1, TextBoxProductName.Text, ComboBoxCatalog.SelectedIndex + 1, Convert.ToInt32(TextBoxAmount.Text), Convert.ToDecimal(TextBoxPrice.Text), "/images/Products/" + System.IO.Path.GetFileName(ImageBox.Source.ToString()), Convert.ToDecimal(TextBoxDiscount.Text), 0, TextAreaDetail.Text, ComboBoxBrand.SelectedIndex + 1));
                    noti.ShowSuccess("Thêm sản phẩm thành công.");
                }
            }
            //MessageBox.Show("/images/Products/"+System.IO.Path.GetFileName(ImageBox.Source.ToString()));
            //MessageBox.Show((ComboBoxCatalog.SelectedItem).ToString());            
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Bạn chắc chắn muốn đật lại?", "Đặt lại", MessageBoxButton.OKCancel);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                TextBoxProductName.Text = "";
                TextBoxAmount.Text = "";
                TextBoxDiscount.Text = "";
                TextBoxPrice.Text = "";
                TextAreaDetail.Text = "";
                ComboBoxBrand.SelectedIndex = -1;
                ComboBoxCatalog.SelectedIndex = -1;
                ImageBox.Source = new BitmapImage(new Uri(@"pack://application:,,,/images/upload.png"));
            }
        }

        private void ComboBoxCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }
    }
}
