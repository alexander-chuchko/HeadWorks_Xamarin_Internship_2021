using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ProfileBook.Service
{
    public class AuthenticationService: IAuthenticationService
    {
        private Regex patternForLogin;
        private Regex patternForPassword;
        public AuthenticationService()
        {
            patternForLogin = new Regex(@"(^[^0-9]{4,16})");
            patternForPassword=new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}");
        }
        public bool IsCheckLoginAndPassword(string login, string password)
        {
            if (patternForLogin.IsMatch(login) && patternForPassword.IsMatch(password))
            {
                return true;
            }
            return false;
        }
        public bool IsCheckLoginAndPasswordAndConfirmPassword(string login, string password, string confirmPassword)
        {
            if (patternForLogin.IsMatch(login) && patternForPassword.IsMatch(password)&&password==confirmPassword)
            {
                return true;
            }
            return false;
        }

    }
}
