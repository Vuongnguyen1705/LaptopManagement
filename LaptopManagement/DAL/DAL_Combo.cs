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
        public string getComboNameByID(int id)
        {
            return db.Comboes.Where(x => x.ID == id).Select(x => x.Combo_Name).SingleOrDefault();
        }
        public int getComboIDByName(string Combo_Name)
        {
            return db.Comboes.Where(x => x.Combo_Name == Combo_Name).Select(x => x.ID).SingleOrDefault();
        }
        public decimal getComboPriceByName(string Combo_Name)
        {
            return db.Comboes.Where(x => x.Combo_Name == Combo_Name).Select(x => (x.totalMoney - (x.totalMoney*x.discount/100))).SingleOrDefault();
        }
    }
}
