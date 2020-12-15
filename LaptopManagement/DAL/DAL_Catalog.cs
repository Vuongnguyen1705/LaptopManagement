using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Catalog
    {
        readonly LaptopShopEntities db = null;
        public DAL_Catalog()
        {
            db = new LaptopShopEntities();
        }
        
        public List<Catalog> getAllCatalog()
        {
            return db.Catalogs.ToList();
        }
        public string getCatalogNameByID(int id)
        {
            return (from r in db.Catalogs
                   where r.ID == id
                   select r.Catalog_Name).SingleOrDefault();
        }
        public int getIDByCatalogName(string catalog)
        {
            return (from r in db.Catalogs
                    where r.Catalog_Name == catalog
                    select r.ID).SingleOrDefault();
        }
    }
}
