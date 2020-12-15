﻿using DAL;
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
        public ObservableCollection<Combo> getAllCombo()
        {
            return dAL_Combo.getAllCombo();
        }
        public void Delete(int id)
        {
            dAL_Combo.Delete(id);
        }
    }
}