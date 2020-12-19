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
    public class BLL_OrderDetail
    {
        DAL_OrderDetail dAL_OrderDetail = new DAL_OrderDetail();


        public void AddOrderDetail(OrderDetail orderdetail)
        {
            dAL_OrderDetail.AddOrderDetail(orderdetail);
        }
        public OrderDetail GetOrderDetailByID(int id)
        {
            return dAL_OrderDetail.GetOrderDetailByID(id);
        }
        public ObservableCollection<OrderDetail> getAllODByOrderID(int id)
        {
            return dAL_OrderDetail.getAllODByOrderID(id);
        }

    }
}
