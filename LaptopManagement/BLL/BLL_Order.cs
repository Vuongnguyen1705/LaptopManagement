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
    public class BLL_Order
    {
        DAL_Order dAL_Order = new DAL_Order();


        public void AddOrder(Order order)
        {
            dAL_Order.AddOrder(order);
        }
        public void UpdateOrder(Order order)
        {
            dAL_Order.UpdateOrder(order);
        }

        public ObservableCollection<Order> getAllOrder()
        {
            return dAL_Order.getAllOrder();
        }

        public void UpdateOrderTotalPrice(Order order)
        {

        }
    }
}
