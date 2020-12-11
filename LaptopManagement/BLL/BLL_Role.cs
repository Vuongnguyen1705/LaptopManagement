using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Role
    {
        DAL_Role dAL_Role = new DAL_Role();
        public string getRoleNameByID(int id)
        {
            return dAL_Role.getRoleNameByID(id);
        }
        public int getIDByRoleName(string role)
        {
            return dAL_Role.getIDByRoleName(role);
        }
    }
}
