using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Product
    {
        readonly LaptopShopEntities db = null;
        public DAL_Product()
        {
            db = new LaptopShopEntities();
        }
        public Product getProductByProductName(string productname)
        {
            return db.Products.Where(x => x.Product_Name == productname).SingleOrDefault();
        }

        public ObservableCollection<Product> getAllProduct()
        {
            return new ObservableCollection<Product>(db.Products.ToList());
        }

        public void deleteProductByID(int id)
        {
            var product = db.Products.Where(x=>x.ID==id).SingleOrDefault();
            db.Products.Remove(product);
            db.SaveChanges();
        }
        public void AddProduct(Product product)
        {
            Product temp = new Product();          
            temp.Product_Name = product.Product_Name;
            temp.Catalog_ID = product.Catalog_ID;
            temp.Amount = product.Amount;
            temp.Price = product.Price;
            temp.Image = product.Image;
            temp.Discount = product.Discount;
            temp.Detail = product.Detail;
            temp.Brand_ID = product.Brand_ID;
            db.Products.Add(temp);
            db.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            Product result = (from p in db.Products
                           where p.ID == product.ID
                           select p).SingleOrDefault();
            result.Product_Name = product.Product_Name;
            result.Catalog_ID = product.Catalog_ID;
            result.Amount = product.Amount;
            result.Price = product.Price;
            result.Image = product.Image;
            result.Discount = product.Discount;
            result.Detail = product.Detail;
            result.Brand_ID = product.Brand_ID;
            db.SaveChanges();
        }

        public string getProductNameByid(int id)
        {
            return db.Products.Where(x => x.ID == id).Select(x => x.Product_Name).SingleOrDefault();
        }
    }
}
