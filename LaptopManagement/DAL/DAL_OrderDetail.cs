using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_OrderDetail
    {
        readonly LaptopShopEntities db = null;
        public DAL_OrderDetail()
        {
            db = new LaptopShopEntities();
        }


        public void AddOrderDetail(OrderDetail orderdetail)
        {
            OrderDetail temp = new OrderDetail();          
            temp.Order_Id = orderdetail.Order_Id;
            temp.Product_Id = orderdetail.Product_Id;
            temp.Combo_Id = orderdetail.Combo_Id;
            temp.Quantity = orderdetail.Quantity;
            temp.Price = orderdetail.Price;
            db.OrderDetails.Add(temp);
            db.SaveChanges();
        }
        public OrderDetail GetOrderDetailByID(int id)
        {
            return db.OrderDetails.Where(x => x.ID == id).SingleOrDefault();
        }

        public ObservableCollection<OrderDetail> getAllODByOrderID(int id)
        {
            return new ObservableCollection<OrderDetail>(db.OrderDetails.Where(x=>x.Order_Id == id).ToList());
        }
        public void deleteOrderDetailByID(int id)
        {
            var orderDetail = db.OrderDetails.Where(x => x.ID == id).SingleOrDefault();
            if (orderDetail != null) 
            { 
                db.OrderDetails.Remove(orderDetail);
                db.SaveChanges();
            }
        }
    }
}
