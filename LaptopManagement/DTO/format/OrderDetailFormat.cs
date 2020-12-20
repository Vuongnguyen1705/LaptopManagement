using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.format
{
    public class OrderDetailFormat
    {


        public OrderDetailFormat(int iD, int oD_ID, string oD_Name, string oD_Type, double oD_TotalMoney, int oD_Quantity)
        {
            ID = iD;
            OD_ID = oD_ID;
            OD_Name = oD_Name;
            OD_Type = oD_Type;
            OD_TotalMoney = oD_TotalMoney;
            OD_Quantity = oD_Quantity;
        }

        public int ID { get; set; }
        public int OD_ID { get; set; }
        public string OD_Name { get; set; }
        public string OD_Type { get; set; }
        public double OD_TotalMoney { get; set; }
        public int OD_Quantity { get; set; }
    }
}
