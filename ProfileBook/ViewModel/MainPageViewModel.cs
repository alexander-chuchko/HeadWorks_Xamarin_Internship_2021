using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.ViewModel
{
    public class MainPageViewModel:BindableBase
    {
        public DelegateCommand NavigationToSingUp { get; set; }
        private readonly INavigationService navigationService;
        //public DelegateCommand NavigateCommand => navigationToSingUp ?? (navigationToSingUp = new DelegateCommand(ExecuteNavigateCommand));
        public MainPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            NavigationToSingUp = new DelegateCommand(ExecuteNavigateCommand);
        }
        async void ExecuteNavigateCommand()
        {
            await navigationService.NavigateAsync("SignUp");
        }
        /*
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        */
    }
}
