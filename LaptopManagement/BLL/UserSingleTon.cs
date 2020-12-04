using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class UserSingleTon
    {
        private static readonly UserSingleTon instance = new UserSingleTon();
        private Employee employee;
        static UserSingleTon()
        {
        }
        private UserSingleTon()
        {
            User = new Employee();
        }
        public static UserSingleTon Instance
        {
            get
            {
                return instance;
            }
        }

        public Employee User { get; set; }
    }
}
