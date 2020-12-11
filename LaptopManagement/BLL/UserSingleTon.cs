using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class UserSingleTon
    {
        private static readonly UserSingleTon instance = new UserSingleTon();
        private User user;
        static UserSingleTon()
        {
        }
        private UserSingleTon()
        {
            User = new User();
        }
        public static UserSingleTon Instance
        {
            get
            {
                return instance;
            }
        }

        public User User { get; set; }
    }
}
