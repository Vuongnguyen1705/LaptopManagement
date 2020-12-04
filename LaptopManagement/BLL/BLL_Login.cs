using DAL;
using System;

namespace BLL
{
    public class BLL_Login
    {
        public int TryLogin(string username, string password)
        {
            DAL_Login login = new DAL_Login();
            if (login.TryLogin(username, password) == 1)
            {
                return 1;
            }
            else if (login.TryLogin(username, password) == 2)
            {
                return 2;
            }
            return 0;
        }
    }
}
