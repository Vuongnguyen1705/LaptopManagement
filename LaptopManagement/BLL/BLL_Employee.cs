using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Employee
    {
        DAL_Employee dAL_Employee = new DAL_Employee();
        public Employee getEmployeeByUsername(string username)
        {
            return dAL_Employee.getEmployeeByUsername(username);
        }
    }
}
