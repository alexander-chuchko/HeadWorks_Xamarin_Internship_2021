using Acr.UserDialogs;
using ProfileBook.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;

namespace ProfileBook.Service
{
    public class AuthenticationService: IAuthenticationService
    {
        //public ObservableCollection<ProfileModel> profileList { get; set; }
        //public ObservableCollection<UserModel> userList { get; set; }
        public AuthenticationService()
        {

        }
        //Method for checking for uniqueness
        public bool IsLoginUniqe(ObservableCollection<UserModel> userList, string login)
        {
            if (userList.Count != 0)
            {
                //проверяем существуют вообще логины
                foreach (var user in userList)
                {
                    if (user.Login == login)
                    {
                        UserDialogs.Instance.Alert("This login is already taken!", "Invalid data entered");
                        return false;
                    }
                }
                return true;
            }
            return true;
        }
    }
}
