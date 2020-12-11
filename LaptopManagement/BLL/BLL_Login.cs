using DAL;
using System;

namespace BLL
{
    public class BLL_Login
    {
        public int TryLogin(string username, string password)
        {
            DAL_Login login = new DAL_Login();
            int role= login.TryLogin(username, Utils.EncryptString(password, Utils.passEncode));
            switch (role)
            {
                case -1:
                    return -1;
                case 1:
                    return 1;
                case 2:
                    return 2;
                case 3:
                    return 3;
                default: 
                    return 0;
            }   
        }
    }
}
