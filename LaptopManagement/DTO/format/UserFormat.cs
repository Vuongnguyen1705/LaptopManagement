using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.format
{
    public class UserFormat
    {
        public UserFormat(int iD, string username, string password, string fullName, string gender, string birthDate, string address, string joinDate, bool isActive, string role)
        {
            ID = iD;
            this.username = username;
            this.password = password;
            this.fullName = fullName;
            this.gender = gender;
            this.birthDate = birthDate;
            this.address = address;
            this.joinDate = joinDate;
            this.isDisable = isActive;
            this.role = role;
        }

        public int ID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string fullName { get; set; }
        public string gender { get; set; }
        public string birthDate { get; set; }
        public string address { get; set; }
        public string joinDate { get; set; }
        public bool isDisable { get; set; }
        public string role { get; set; }
    }
}
