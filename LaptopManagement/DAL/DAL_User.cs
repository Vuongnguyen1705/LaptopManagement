using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_User
    {
        readonly LaptopShopEntities db = null;
        public DAL_User()
        {
            db = new LaptopShopEntities();
        }
        public User getUserByUsername(string username)
        {
            return db.Users.Where(x => x.username == username).SingleOrDefault();          
        }

        public User getUserByID(int id)
        {
            return db.Users.Where(x => x.ID == id).SingleOrDefault();
        }

        public ObservableCollection<User> getAllUser()
        {
            return new ObservableCollection<User>(db.Users.ToList());
        }

        public void deleteUserByID(int id)
        {
            var employee = db.Users.Where(x=>x.ID==id).SingleOrDefault();
            db.Users.Remove(employee);
            db.SaveChanges();
        }

        public string getGenderByValue(bool gender)
        {
            if (gender == true)
            {
                return "Nam";
            }
            return "Nữ";
        }
        public bool getValueByGender(string gender)
        {
            if (gender.ToLower().Equals("nam"))
            {
                return true;
            }
            return false;
        }

        public List<string> getGender()
        {
            var gender = new List<string>();
            gender.Add("Nam");
            gender.Add("Nữ");
            return gender;
        }

        public string getActive(bool active)
        {
            if (active == true)
            {
                return "Đang hoạt động";
            }
            return "Đã khóa";
        }

        public void DisableUser(int UserID)
        {
            User result = (from p in db.Users
                             where p.ID == UserID
                             select p).SingleOrDefault();

            result.isDisable = true;

            db.SaveChanges();
        }

        public void EnableUser(int UserID)
        {
            User result = (from p in db.Users
                           where p.ID == UserID
                           select p).SingleOrDefault();

            result.isDisable = false;

            db.SaveChanges();
        }

        public void Update(User user)
        {
            User result = (from p in db.Users  
                           where p.ID==user.ID
                           select p).SingleOrDefault();
            result.firstName = user.firstName;
            result.lastName = user.lastName;
            result.isDisable = user.isDisable;
            result.address = user.address;
            result.birthDate = user.birthDate;
            result.gender = user.gender;
            result.joinDate = user.joinDate;
            result.password = user.password;
            result.Role_ID = user.Role_ID;
            db.SaveChanges();
        }

        
        public string getStatusByValue(bool value)
        {
            if (value == true)
            {
                return "Đã khóa";
            }
            return "Đang hoạt động";
        }

        public bool getValueByStatus(string status)
        {
            if (status.ToLower().Equals("đã khóa"))
            {
                return true;
            }
            return false;
        }
 
        public void AddUser(User user)
        {            
            User temp = new User();
            temp.firstName = user.firstName;
            temp.lastName = user.lastName;
            temp.isDisable = user.isDisable;
            temp.address = user.address;
            temp.birthDate = user.birthDate;
            temp.gender = user.gender;
            temp.joinDate = user.joinDate;
            temp.password = user.password;
            temp.Role_ID = user.Role_ID;
            temp.username = user.username;            
            db.Users.Add(temp);
            db.SaveChanges();
        }
    }
}
