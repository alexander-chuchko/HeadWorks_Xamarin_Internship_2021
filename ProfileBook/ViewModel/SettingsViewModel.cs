using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.ViewModel
{
    public class SettingsViewModel: BindableBase
    {
        private readonly INavigationService navigationService;
        public SettingsViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

    }
}
