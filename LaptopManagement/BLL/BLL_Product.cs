using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Product
    {
        DAL_Product dAL_Product = new DAL_Product();
        public Product getProductByProductName(string productname)
        {
            return dAL_Product.getProductByProductName(productname);
        }

        public ObservableCollection<Product> getAllProduct()
        {
            return dAL_Product.getAllProduct();
        }

        public void deleteProductByID(int id)
        {
            dAL_Product.deleteProductByID(id);
        }

        public void AddProduct(Product product)
        {
            dAL_Product.AddProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            dAL_Product.UpdateProduct(product);
        }

        public string getProductNameByid(int id)
        {
            return dAL_Product.getProductNameByid(id);
        }


    }
}
