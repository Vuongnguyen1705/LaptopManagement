using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Combo
    {
        DAL_Combo dAL_Combo = new DAL_Combo();

        public Combo getComboByID(int id)
        {
            return dAL_Combo.getComboByID(id);
        }

        public ObservableCollection<Combo> getAllCombo()
        {
            return dAL_Combo.getAllCombo();
        }
        public void Delete(int id)
        {
            dAL_Combo.Delete(id);
        }

        public void AddCombo(Combo combo)
        {
            dAL_Combo.AddCombo(combo);
        }

        public string getComboNameByID(int id)
        {
            return dAL_Combo.getComboNameByID(id);
        }

        public int getIDByComboName(string name)
        {
            return dAL_Combo.getIDByComboName(name);
        }

        public void Update(Combo combo)
        {
            dAL_Combo.Update(combo);
        }
    }
}
