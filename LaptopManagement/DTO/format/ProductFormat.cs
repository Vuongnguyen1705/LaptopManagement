using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.format
{
    public class ProductFormat
    {
        public ProductFormat(int iD, string Product_Name, string catalog, int amount, decimal price, string Image, decimal discount, decimal discountMoney, string Detail, string brand)
        {
            ID = iD;
            this.Product_Name = Product_Name;
            this.catalog = catalog;
            Amount = amount;
            Price = price;
            this.Image = Image;
            Discount = discount;
            DiscountMoney = discountMoney;
            this.Detail = Detail;
            this.brand = brand;
        }

        public int ID { get; set; }
        public string Product_Name { get; set; }
        public string catalog { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountMoney { get; set; }
        public string Detail { get; set; }
        public string brand { get; set; }
    }
}
