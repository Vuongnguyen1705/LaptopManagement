using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Brand
    {
        readonly LaptopShopEntities db = null;
        public DAL_Brand()
        {
            db = new LaptopShopEntities();
        }
        public List<Brand> getAllBrand()
        {
            return db.Brands.ToList();
        }
        public string getBrandNameByID(int id)
        {
            return (from r in db.Brands
                   where r.ID == id
                   select r.Brand_Name).SingleOrDefault();
        }
        public int getIDByBrandName(string brand)
        {
            return (from r in db.Brands
                    where r.Brand_Name == brand
                    select r.ID).SingleOrDefault();
        }
    }
}
