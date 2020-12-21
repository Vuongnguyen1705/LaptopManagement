using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
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

        public Combo getComboByID(int id)
        {
            return db.Comboes.Where(x => x.ID == id).SingleOrDefault();
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

        public void AddCombo(Combo combo)
        {
            Combo temp = new Combo();
            temp.Combo_Name = combo.Combo_Name;
            temp.Image = combo.Image;
            temp.Product_List = combo.Product_List;
            temp.startDate = combo.startDate;
            temp.endDate = combo.endDate;
            temp.totalMoney = combo.totalMoney;
            temp.discount = combo.discount;
            db.Comboes.Add(temp);
            db.SaveChanges();           
        }

        public void Update(Combo combo)
        {
            Combo temp = (from c in db.Comboes
                           where c.ID == combo.ID
                           select c).SingleOrDefault();
            temp.Combo_Name = combo.Combo_Name;
            temp.Product_List = combo.Product_List;
            temp.startDate = combo.startDate;
            temp.endDate = combo.endDate;
            temp.totalMoney = combo.totalMoney;
            temp.discount = combo.discount;
            db.SaveChanges();
        }

        public string getComboNameByID(int id)
        {
            return (from c in db.Comboes
                    where c.ID == id
                    select c.Combo_Name).SingleOrDefault();
        }

        public int getIDByComboName(string name)
        {
            return (from c in db.Comboes
                    where c.Combo_Name == name
                    select c.ID).SingleOrDefault();
        }
        public int getComboIDByName(string Combo_Name)
        {
            return db.Comboes.Where(x => x.Combo_Name == Combo_Name).Select(x => x.ID).SingleOrDefault();
        }
        public decimal getComboPriceByName(string Combo_Name)
        {
            return (decimal)db.Comboes.Where(x => x.Combo_Name == Combo_Name).Select(x => x.totalMoney - (x.totalMoney * x.discount / 100)).SingleOrDefault();
        }
    }
}
