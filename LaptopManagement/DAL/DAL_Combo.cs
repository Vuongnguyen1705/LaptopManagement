using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Combo
    {
        readonly LaptopShopEntities db = null;

        public DAL_Combo()
        {
            db = new LaptopShopEntities();
        }

        public ObservableCollection<Combo> getAllCombo()
        {
            return new ObservableCollection<Combo>(db.Comboes.ToList());
        }

        public void Delete(int id)
        {
            var combo = db.Comboes.Where(x => x.ID == id).SingleOrDefault();
            db.Comboes.Remove(combo);
            db.SaveChanges();
        }
    }
}
