using BLL;
using DTO;
using DTO.format;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
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
    /// Interaction logic for AddComboPage.xaml
    /// </summary>
    public partial class AddComboPage : Page
    {
        private BLL_Product bLL_Product;
        private BLL_Combo bLL_Combo;
        private Dictionary<string, decimal> dic = new Dictionary<string, decimal>();
        private double priceProduct = 0;
        private List<int> productListID = new List<int>();
        private ToastViewModel _vm;
        private int _idCombo;
        private string productid;
        private bool flagComboName = true, flagDiscount = true, flagTotalMoney = true;
        string filePath;
        private string destinationDir;
        public AddComboPage(int idCombo)
        {
            InitializeComponent();
            _idCombo = idCombo;
            bLL_Product = new BLL_Product();
            bLL_Combo = new BLL_Combo();
            _vm = new ToastViewModel();
            //ImageCombo.Source = new BitmapImage(new Uri("pack://application:,,,/LaptopManagement;component/LaptopShop/LaptopShop/Assets/Layout2/images/Headphone/EH469RGB.png", UriKind.RelativeOrAbsolute));
            SetVisiable();
            SetInfo();
        }

        private void SetInfo()
        {
            if (_idCombo != -1)
            {
                Combo combo = bLL_Combo.getComboByID(_idCombo);
                TextBoxComboName.Text = combo.Combo_Name;
                DatePickerStartDate.Text = combo.startDate.ToShortDateString();
                DatePickerEndDate.Text = combo.endDate.ToShortDateString();
                TextBoxDiscount.Text = combo.discount.ToString();
                TextBoxTotalMoney.Text = combo.totalMoney.ToString("#,##0.00");
                priceProduct = combo.totalMoney;
                productid = combo.Product_List + ";";                
                TextBlockPriceCombo.Text = (priceProduct - (priceProduct * Int32.Parse(TextBoxDiscount.Text) / 100)).ToString("#,##0.00");
                string[] temp_productList = combo.Product_List.Split(';');
                foreach (var i in temp_productList)
                {
                    Chip chip = new Chip ();
                    chip.Content = bLL_Product.getProductNameByid(int.Parse(i));
                    chip.Margin = new Thickness(5);
                    productListID.Add(Convert.ToInt32(i));
                    //chip.DeleteClick += Chip_OnDeleteClick;
                    WrapPanelProductSelected.Children.Add(chip);                    
                }
                ButtonAddCombo.Content = "Cập nhật";

            }
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
                    list.Add(new ProductFormat(false, item.ID, item.Product_Name, "", item.Amount, Convert.ToDouble(item.Price), item.Image, (int)item.Discount, item.Detail, ""));
                }
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    ImageAwesomeLoading.Visibility = Visibility.Collapsed;
                    GridRoot.Visibility = Visibility.Visible;
                    DataGridCombo.ItemsSource = list;
                    //Filter();
                }), DispatcherPriority.Background);
            }).Start();

        }

        private void CheckBoxSelect_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox check = sender as CheckBox;
            ProductFormat format = check.DataContext as ProductFormat;
            dic.Add(format.Product_Name, (decimal)format.Price);
            productListID.Add(format.ID);
            format.isCheck = true;
        }

        private void CheckBoxSelect_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox check = sender as CheckBox;
            ProductFormat format = check.DataContext as ProductFormat;
            dic.Remove(format.Product_Name);
            productListID.Remove(format.ID);
            format.isCheck = false;
        }

        private void ButtonAddCombo_Click(object sender, RoutedEventArgs e)
        {
            
            if (_idCombo == -1)
            {
                productid = "";
                foreach (var item in productListID)
                {
                    productid += item + ";";
                }
                if (flagDiscount == true && flagComboName == true)
                {
                    if (flagTotalMoney == false)
                    {
                        _vm.ShowError("Vui lòng chọn sản phẩm cho Combo");
                    }
                    else
                    {
                        //MessageBox.Show("images/Combo/" + System.IO.Path.GetFileName(filePath));
                        System.IO.File.Copy(filePath, destinationDir + System.IO.Path.GetFileName(filePath), true);
                        bLL_Combo.AddCombo(new Combo(0, "images/Combo/" + System.IO.Path.GetFileName(filePath), TextBoxComboName.Text, productid.Remove(productid.Length - 1), DateTime.Parse(DatePickerStartDate.Text), DateTime.Parse(DatePickerEndDate.Text), Double.Parse(TextBoxTotalMoney.Text), Int32.Parse(TextBoxDiscount.Text)));
                        _vm.ShowSuccess("Thêm Combo thành công");
                    }
                }
                else
                {
                    _vm.ShowError("Vui lòng nhập đúng thông tin");
                }
            }
            else
            {
                if (productListID.Count != 0)
                {
                    productid = "";
                    foreach (var item in productListID)
                    {
                        productid += item + ";";
                    }
                }
                if (flagComboName == true && flagDiscount == true)
                {
                    if (flagTotalMoney == false)
                    {
                        _vm.ShowError("Vui lòng chọn sản phẩm cho Combo");
                    }
                    else
                    {
                        System.IO.File.Copy(filePath, destinationDir + System.IO.Path.GetFileName(filePath), true);
                        bLL_Combo.Update(new Combo(_idCombo,"images/Combo/"+ System.IO.Path.GetFileName(filePath), TextBoxComboName.Text, productid.Remove(productid.Length - 1), DateTime.Parse(DatePickerStartDate.Text), DateTime.Parse(DatePickerEndDate.Text), Double.Parse(TextBoxTotalMoney.Text), Int32.Parse(TextBoxDiscount.Text)));
                        _vm.ShowSuccess("Cập nhật thành công");
                    }
                }
                else
                {
                    _vm.ShowError("Vui lòng nhập đúng thông tin");
                }
            }

        }

        private void Chip_OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var currentChip = (Chip)sender;
            WrapPanelProductSelected.Children.Remove(currentChip);
        }

        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            WrapPanelProductSelected.Children.Clear();
            Chip chip;
            priceProduct = 0;
            foreach (var item in dic)
            {
                chip = new Chip ();
                chip.Content = item.Key;
                priceProduct = (double)((int)priceProduct + item.Value);
                chip.Margin = new Thickness(5);                
                WrapPanelProductSelected.Children.Add(chip);
            }
            TextBoxTotalMoney.Text = priceProduct.ToString("#,##0.00");
            if (TextBoxDiscount.Text != "" && Int32.Parse(TextBoxDiscount.Text) != 0 && priceProduct != 0)
            {
                TextBlockPriceCombo.Text = (priceProduct - (priceProduct * Int32.Parse(TextBoxDiscount.Text) / 100)).ToString("#,##0.00");
            }
            else
            {
                TextBlockPriceCombo.Text = "";
            }

        }

        private void ButtonReSelect_Click(object sender, RoutedEventArgs e)
        {
            WrapPanelProductSelected.Children.Clear();
            TextBlockPriceCombo.Text = "";
            TextBoxTotalMoney.Text = "";
            productid = "";
            productListID.Clear();
            dic.Clear();
            SetVisiable();
        }

        private void TextBoxDiscount_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBoxDiscount.Text != "" && Int32.Parse(TextBoxDiscount.Text) != 0 && priceProduct != 0)
            {
                TextBlockPriceCombo.Text = (priceProduct - (priceProduct * Int32.Parse(TextBoxDiscount.Text) / 100)).ToString("#,##0.00");
            }
            else
            {
                TextBlockPriceCombo.Text = "";
            }
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (e.Handled != regex.IsMatch(e.Text))
            {
                e.Handled = regex.IsMatch(e.Text);
            }
        }

        private void TextBoxComboName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                TextBlockComboNameError.Visibility = Visibility.Visible;
                TextBlockComboNameError.Text = "Không được để trống ô này";
                flagComboName = false;
            }
            else
            {
                if (!Regex.IsMatch(textBox.Text, @"^[a-z0-9A-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\s_]+$"))
                {
                    TextBlockComboNameError.Visibility = Visibility.Visible;
                    TextBlockComboNameError.Text = "Không được nhập ký tự đặt biệt";
                    flagComboName = false;
                }
                else
                {
                    bool flag = true;
                    foreach(var item in bLL_Combo.getAllCombo())
                    {
                        if (item.Combo_Name.Equals(TextBoxComboName.Text))
                        {                           
                            flag = false;
                            break;
                        }
                    }
                    if (flag == true)
                    {
                        TextBlockComboNameError.Visibility = Visibility.Collapsed;
                        flagComboName = true;
                    }
                    else
                    {
                        if (_idCombo == -1)
                        {
                            TextBlockComboNameError.Visibility = Visibility.Visible;
                            TextBlockComboNameError.Text = "Tên combo đã tồn tại";
                            flagComboName = false;
                        }
                    }
                    
                }
            }
        }

        private void TextBoxTotalMoney_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                flagTotalMoney = false;
            }
            else
            {
                flagTotalMoney = true;
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
                if(Convert.ToInt32(textBox.Text)<1 || Convert.ToInt32(textBox.Text) > 99)
                {
                    TextBlockDiscountError.Visibility = Visibility.Visible;
                    TextBlockDiscountError.Text = "Giảm giá từ 1 đến 99";
                    flagDiscount = false;
                }
                else
                {
                    TextBlockDiscountError.Visibility = Visibility.Collapsed;
                    flagDiscount = true;
                }
            }
        }

        private void ImageCombo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                ImageCombo.Source = new BitmapImage(fileUri);
                filePath = fileUri.ToString().Remove(0, 8);
                //MessageBox.Show(filePath);
                destinationDir = "../../../../LaptopShop/LaptopShop/Assets/Layout2/images/Combo/";                
                //MessageBox.Show(destinationDir + System.IO.Path.GetFileName(filePath));
            }
        }
    }
}
