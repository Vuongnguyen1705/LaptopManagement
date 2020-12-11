using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Role
    {
        readonly LaptopShopEntities db = null;
        public DAL_Role()
        {
            db = new LaptopShopEntities();
        }

        public string getRoleNameByID(int id)
        {
            return (from r in db.Roles
                   where r.ID == id
                   select r.Role_Name).SingleOrDefault();
        }
        public int getIDByRoleName(string role)
        {
            return (from r in db.Roles
                    where r.Role_Name == role
                    select r.ID).SingleOrDefault();
        }
    }
}
