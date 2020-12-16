using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.format
{
    public class ProductFormat
    {
        public ProductFormat(bool isCheck,int iD, string Product_Name, string catalog, int amount, decimal price, string Image, decimal discount, string Detail, string brand)
        {
            this.isCheck = isCheck;
            ID = iD;
            this.Product_Name = Product_Name;
            this.catalog = catalog;
            Amount = amount;
            Price = price;
            this.Image = Image;
            Discount = discount;
            this.Detail = Detail;
            this.brand = brand;
        }

        public bool isCheck { get; set; }
        public int ID { get; set; }
        public string Product_Name { get; set; }
        public string catalog { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public decimal Discount { get; set; }
        public string Detail { get; set; }
        public string brand { get; set; }
    }
}
