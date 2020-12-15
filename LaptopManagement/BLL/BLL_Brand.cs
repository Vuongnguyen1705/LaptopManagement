using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Brand
    {
        DAL_Brand dAL_Brand = new DAL_Brand();
        public string getBrandNameByID(int id)
        {
            return dAL_Brand.getBrandNameByID(id);
        }
        public int getIDByBrandName(string role)
        {
            return dAL_Brand.getIDByBrandName(role);
        }
        public List<Brand> getAllBrand()
        {
            return dAL_Brand.getAllBrand();
        }
    }
}
