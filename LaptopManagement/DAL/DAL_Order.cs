using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Order
    {
        readonly LaptopShopEntities db = null;
        public DAL_Order()
        {
            db = new LaptopShopEntities();
        }

        public ObservableCollection<Order> getAllOrder()
        {
            return new ObservableCollection<Order>(db.Orders.ToList());
        }

        public void AddOrder(Order order)
        {
            Order temp = new Order();          
            temp.Date = order.Date;
            temp.Total_Price = order.Total_Price;
            temp.User_Id = order.User_Id;
            temp.Status = order.Status;
            db.Orders.Add(temp);
            db.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            Order result = (from p in db.Orders
                              where p.ID == order.ID
                              select p).SingleOrDefault();
            result.Date = order.Date;
            result.Total_Price = order.Total_Price;
            result.User_Id = order.User_Id;
            result.Status = order.Status;
            db.SaveChanges();
        }
    }
}
