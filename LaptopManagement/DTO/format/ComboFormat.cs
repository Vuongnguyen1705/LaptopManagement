using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.format
{
    public class ComboFormat
    {
        public ComboFormat(int iD, string combo_Name, string product_List, string startDate, string endDate, decimal totalMoney, int discount)
        {
            ID = iD;
            Combo_Name = combo_Name;
            Product_List = product_List;
            this.startDate = startDate;
            this.endDate = endDate;
            this.totalMoney = totalMoney;
            this.discount = discount;
        }

        public int ID { get; set; }
            public string Combo_Name { get; set; }
            public string Product_List { get; set; }
            public string startDate { get; set; }
            public string endDate { get; set; }
            public decimal totalMoney { get; set; }
            public int discount { get; set; }
    }
}
