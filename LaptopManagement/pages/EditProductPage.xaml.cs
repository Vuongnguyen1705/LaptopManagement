﻿using DTO;
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
    public partial class EditProductPage : Page
    {
        private BLL_Catalog bLL_Catalog = new BLL_Catalog();
        private BLL_Brand bLL_Brand = new BLL_Brand();
        private BLL_Product bLL_Product = new BLL_Product();
        private int id;
        private bool flagProductName = false, flagDetail = false, flagAmount = false, flagDiscount = false, flagPrice = false;
        private ToastViewModel noti = new ToastViewModel();
        private string filePath;
        private string destinationDir;
        public EditProductPage(string name)
        {
            InitializeComponent();
            ShowProductCatalog();
            ShowProductBrand();
            ShowInfo(name);
            id = bLL_Product.getProductByProductName(name).ID;
        }

        private void ShowInfo(string name)
        {
            Product p = bLL_Product.getProductByProductName(name);
            TextBoxProductName.Text = name;
            TextBoxPrice.Text = p.Price.ToString();
            TextBoxAmount.Text = p.Amount.ToString();
            TextBoxDiscount.Text = p.Discount.ToString();
            TextAreaDetail.Text = p.Detail.ToString();
            ComboBoxCatalog.SelectedIndex = p.Catalog_ID - 1;
            ComboBoxBrand.SelectedIndex = (int)p.Brand_ID - 1;
            //ImageBox.Source = new BitmapImage (new Uri(p.Image));   --Lỗi: đường dẫn cứng thì đọc, đường dẫn động error
        }
        private void ShowProductCatalog()
        {
            ComboBoxCatalog.ItemsSource = bLL_Catalog.getAllCatalog();
        }
        private void ShowProductBrand()
        {
            ComboBoxBrand.ItemsSource = bLL_Brand.getAllBrand();
        }

        private void ImageBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {

                Uri fileUri = new Uri(openFileDialog.FileName);
                ImageBox.Source = new BitmapImage(fileUri);
                string filePath = fileUri.ToString().Remove(0, 8);
                destinationDir = "../../../../LaptopShop/LaptopShop/Assets/Layout2/images/";
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

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("name: " + flagProductName + " price: " + flagPrice + " discount: " + flagDiscount + " detail:" + flagDetail);

            if (flagAmount == true && flagDetail == true && flagDiscount == true && flagPrice == true && flagProductName == true)
            {
                string folder = "";
                switch (ComboBoxCatalog.SelectedIndex +1)
                {
                    case 1:
                        folder = "PC/";
                        break;
                    case 2:
                        folder = "Keyboard/";
                        break;
                    case 3:
                        folder = "Mouse/";
                        break;
                    case 4:
                        folder = "Headphone/";
                        break;
                    case 5:
                        folder = "Speaker/";
                        break;
                    case 6:
                        folder = "Laptop/";
                        break;
                }
                string linkImage = "";
                if (destinationDir == null)
                {
                    linkImage = "";
                }
                else
                {
                    linkImage = "/images/" + folder + System.IO.Path.GetFileName(filePath);
                    System.IO.File.Copy(filePath, destinationDir + folder + System.IO.Path.GetFileName(filePath), true);
                }
                bLL_Product.UpdateProduct(new Product(id, TextBoxProductName.Text, ComboBoxCatalog.SelectedIndex + 1, Convert.ToInt32(TextBoxAmount.Text), Convert.ToDecimal(TextBoxPrice.Text), "/images/" + folder + System.IO.Path.GetFileName(filePath), Convert.ToInt32(TextBoxDiscount.Text), TextAreaDetail.Text, ComboBoxBrand.SelectedIndex + 1));
                noti.ShowSuccess("Cập nhật sản phẩm thành công.");
            }
            else
            {
                noti.ShowError("Vui lòng nhập đầy đủ thông tin");
            }
            //MessageBox.Show("/images/Products/"+System.IO.Path.GetFileName(ImageBox.Source.ToString()));
            //MessageBox.Show((ComboBoxCatalog.SelectedItem).ToString());            
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Bạn chắc chắn muốn đặt lại?", "Đặt lại", MessageBoxButton.OKCancel);
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
        private void TextBoxProductName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                TextBlockProductNameError.Visibility = Visibility.Visible;
                TextBlockProductNameError.Text = "Không được để trống ô này";
                flagProductName = false;
            }
            else
            {
                if (!Regex.IsMatch(textBox.Text, @"^[a-z0-9A-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\s\W]+$"))
                {
                    TextBlockProductNameError.Visibility = Visibility.Visible;
                    TextBlockProductNameError.Text = "Không được nhập ký tự đặt biệt";
                    flagProductName = false;
                }
                else
                {
                    TextBlockProductNameError.Visibility = Visibility.Collapsed;
                    flagProductName = true;
                }
            }
        }

        private void TextBoxAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                TextBlockAmountError.Visibility = Visibility.Visible;
                TextBlockAmountError.Text = "Không được để trống ô này";
                flagAmount = false;
            }
            else
            {
                if (Convert.ToInt32(textBox.Text) < 0)
                {
                    TextBlockAmountError.Visibility = Visibility.Visible;
                    TextBlockAmountError.Text = "Không được nhập số âm";
                    flagAmount = false;
                }
                else
                {
                    TextBlockAmountError.Visibility = Visibility.Collapsed;
                    flagAmount = true;
                }
            }
        }

        private void TextBoxDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                TextBlockDiscountError.Visibility = Visibility.Visible;
                TextBlockDiscountError.Text = "Không được để trống ô này";
                flagDiscount = false;
            }
            else
            {
                if (Convert.ToInt32(textBox.Text) < 0 || Convert.ToInt32(textBox.Text) > 99)
                {
                    TextBlockDiscountError.Visibility = Visibility.Visible;
                    TextBlockDiscountError.Text = "Giảm giá từ 0 đến 99";
                    flagDiscount = false;
                }
                else
                {
                    TextBlockDiscountError.Visibility = Visibility.Collapsed;
                    flagDiscount = true;
                }
            }
        }

        private void TextBoxPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                TextBlockPriceError.Visibility = Visibility.Visible;
                TextBlockPriceError.Text = "Không được để trống ô này";
                flagPrice = false;
            }
            else
            {
                if (Convert.ToInt32(textBox.Text) < 0)
                {
                    TextBlockPriceError.Visibility = Visibility.Visible;
                    TextBlockPriceError.Text = "Không được nhập số âm";
                    flagPrice = false;
                }
                else
                {
                    TextBlockPriceError.Visibility = Visibility.Collapsed;
                    flagPrice = true;
                }
            }
        }

        private void TextAreaDetail_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                TextBlockDetailError.Visibility = Visibility.Visible;
                TextBlockDetailError.Text = "Không được để trống ô này";
                flagDetail = false;
            }
            else
            {
                TextBlockDetailError.Visibility = Visibility.Collapsed;
                flagDetail = true;
            }
        }
    }
}
