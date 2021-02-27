using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.ViewModel
{
    public class MainPageViewModel:BindableBase
    {
        //private string _name;
        public DelegateCommand NavigationToSingUp { get; set; }
        private INavigationService navigationService;
        public MainPageViewModel(INavigationService navigationService)//:base (navigationService)
        {
           //Title = "Main Page";
            this.navigationService = navigationService;
            NavigationToSingUp = new DelegateCommand(NavigationToSingUpCall);
        }
        public void NavigationToSingUpCall() 
        {
            navigationService.NavigateAsync("PageSingUp");
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
