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
    public class BLL_User
    {
        DAL_User dAL_User = new DAL_User();
        public User getUserByUsername(string username)
        {
            return dAL_User.getUserByUsername(username);
        }

        public User getUserByID(int id)
        {
            return dAL_User.getUserByID(id);
        }

        public ObservableCollection<User> getAllUser()
        {
            return dAL_User.getAllUser();
        }

        public void deleteUserByID(int id)
        {
            dAL_User.deleteUserByID(id);
        }

        public string getGender(bool gender)
        {
            return dAL_User.getGenderByValue(gender);
        }
        public bool getValueByGender(string gender)
        {
            return dAL_User.getValueByGender(gender);
        }
        public string getActive(bool active)
        {
            return dAL_User.getActive(active);
        }

        public void DisableUser(int UserID)
        {
            dAL_User.DisableUser(UserID);
        }

        public void EnableUser(int UserID)
        {
            dAL_User.EnableUser(UserID);
        }

        public List<string> getGender()
        {
            return dAL_User.getGender();
        }

        public void Update(User user)
        {
            dAL_User.Update(user);
        }

        public string getStatusByValue(bool value)
        {
            return dAL_User.getStatusByValue(value);
        }

        public bool getValueByStatus(string status)
        {
            return dAL_User.getValueByStatus(status);
        }
        public void AddUser(User user)
        {
            dAL_User.AddUser(user);
        }
    }
}
