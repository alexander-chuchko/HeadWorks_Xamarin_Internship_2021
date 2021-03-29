using ProfileBook.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ProfileBook.Service
{
    public interface IAuthenticationService
    {
        int Id { get; set; }
        bool IsLoginUniqe(ObservableCollection<UserModel> userList, string login);
        bool IsRelevantLoginAndPassword(ObservableCollection<UserModel> userList, string login, string password);
    }
}
