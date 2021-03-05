﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Acr.UserDialogs;

namespace ProfileBook
{
    public static class Helper
    {
        private static Regex patternForLogin;
        private static Regex patternForPassword;
        const int maxSignsForLoginAndPassword=16;
        const int minSignsForLogin = 4;
        const int minSignsForPassword=8;
        static Helper()
        {
            patternForLogin = new Regex(@"(^[^0-9]{4,16})");
            patternForPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}");
        }
        public static bool IsCheckLoginAndPassword(string login, string password)
        {
            if (patternForLogin.IsMatch(login))
            {
                if (patternForPassword.IsMatch(password))
                {
                    return true;
                }
                else
                {
                    UserDialogs.Instance.Alert("Invalid login or password!", "Invalid data entered");
                    return false;
                }
            }
            else
            {
                UserDialogs.Instance.Alert("Invalid login or password!", "Invalid data entered");
                return false;
            }
        }
        public static bool IsValidatedLoginAndPasswordAndConfirmPassword(string login, string password, string confirmPassword)
        {
            if(patternForLogin.IsMatch(login))
            {
                if(password == confirmPassword)
                {
                    if(patternForPassword.IsMatch(password))
                    {
                        return true;
                    }
                    else
                    {
                        UserDialogs.Instance.Alert("Password must be at least 8 and no more than 16 characters and contain at least one uppercase letter, one lowercase letter and one number", "Invalid data entered");
                        return false;
                    }
                }
                else
                {
                    UserDialogs.Instance.Alert("The values in the Password and ConfirmPassword fields must match!", "Invalid data entered");
                    return false;
                }
            }
            else
            {
                UserDialogs.Instance.Alert("Login must be at least 4 and no more than 16 characters and not start with numbers!", "Invalid data entered");
                return false;
            } 
        }
    }
}
