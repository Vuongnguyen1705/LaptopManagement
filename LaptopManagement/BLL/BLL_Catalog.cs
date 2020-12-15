using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Catalog
    {
        DAL_Catalog dAL_Catalog = new DAL_Catalog();
        public string getCatalogNameByID(int id)
        {
            return dAL_Catalog.getCatalogNameByID(id);
        }
        public int getIDByCatalogName(string catalog)
        {
            return dAL_Catalog.getIDByCatalogName(catalog);
        }
        public List<Catalog> getAllCatalog()
        {
            return dAL_Catalog.getAllCatalog();
        }
    }
}
