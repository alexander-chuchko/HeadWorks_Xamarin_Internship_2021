using ProfileBook.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ProfileBook.Service
{
    public interface IAuthenticationService
    {
        bool IsLoginUniqe(ObservableCollection<UserModel> userList, string login);
    }
}
