using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.format
{
    public class ProductSelectionFormat
    {
        public ProductSelectionFormat(int iD, string name, double price, string type)
        {
            ID = iD;
            Name = name;
            Price = price;
            Type = type;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
    }
}
