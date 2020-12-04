using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Employee
    {
        readonly LaptopShopEntities db = null;
        public DAL_Employee()
        {
            db = new LaptopShopEntities();
        }
        public Employee getEmployeeByUsername(string username)
        {
            return db.Employees.Where(x => x.username == username).SingleOrDefault();
           
        }
    }
}
