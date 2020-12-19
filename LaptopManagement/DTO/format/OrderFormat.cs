using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.format
{
    public class OrderFormat
    {
        public OrderFormat(int iD, int customer_Id, DateTime date, double total_Price, string customer_Name, string status)
        {
            ID = iD;
            Customer_Id = customer_Id;
            Date = date;
            Total_Price = total_Price;
            Customer_Name = customer_Name;
            Status = status;
        }

        public int ID { get; set; }
        public int Customer_Id { get; set; }
        public DateTime Date { get; set; }
        public double Total_Price { get; set; }
        public string  Customer_Name { get; set; }
        public string Status { get; set; }
    }
}
