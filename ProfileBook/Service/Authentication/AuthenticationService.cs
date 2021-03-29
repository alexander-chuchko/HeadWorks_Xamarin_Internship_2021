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
        private int _id;
        //public ObservableCollection<ProfileModel> profileList { get; set; }
        //public ObservableCollection<UserModel> userList { get; set; }
        public AuthenticationService()
        {

        }

        //Method det id
        public int Id
        {
            set { _id = value; }
            get { return _id; }
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
        //Method for checking if the username and password match with the database
        public bool IsRelevantLoginAndPassword(ObservableCollection<UserModel> userList, string login, string password)
        {
            if (userList.Count != 0)
            {
                //проверяем существуют вообще логины
                foreach (var user in userList)
                {
                    if (user.Login == login&&user.Password== password)
                    {
                        //Set in property id user
                        Id = user.Id;
                        return true;
                    }
                    else
                    {
                        UserDialogs.Instance.Alert("This login is already taken!", "Invalid data entered");
                        return false;

                    }
                }
                return true;
            }
            return false;
        }

    }
}
