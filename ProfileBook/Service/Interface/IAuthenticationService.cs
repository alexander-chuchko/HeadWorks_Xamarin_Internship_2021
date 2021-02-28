using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Service
{
    public interface IAuthenticationService
    {
        bool IsCheckLoginAndPassword(string login, string password);
        bool IsCheckLoginAndPasswordAndConfirmPassword(string login, string password, string confirmPassword);
    }
}
