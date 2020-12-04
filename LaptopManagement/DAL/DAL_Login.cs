using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Login
    {
        readonly LaptopShopEntities db = null;
        public DAL_Login()
        {
            db = new LaptopShopEntities();
        }

        public int TryLogin(string username, string password)
        {
            var query = db.Employees.Where(x => x.username == username && x.password == password).SingleOrDefault();
            if (query != null)
            {
                if (query.Role_ID == 1)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            return 0;

        }
    }
}
