using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;


namespace ProfileBook.ViewModel
{
    class SingUpViewModel: BindableBase
    {
        public DelegateCommand GoBackCommand { get; set; }
        private readonly INavigationService navigationService;
        //public DelegateCommand NavigateCommand => navigationToSingUp ?? (navigationToSingUp = new DelegateCommand(ExecuteNavigateCommand));
        public SingUpViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            GoBackCommand = new DelegateCommand(ExecuteNavigateCommand);
        }
        async void ExecuteNavigateCommand()
        {
            await navigationService.GoBackAsync();
        }
        /*
        public DelegateCommand navigationToSingUp;
        private readonly INavigationService navigationService;
        public DelegateCommand NavigateCommand => navigationToSingUp ?? (navigationToSingUp = new DelegateCommand(ExecuteNavigateCommand));
        public SingUpViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        async void ExecuteNavigateCommand()
        {
            //await navigationService.NavigateAsync($"{nameof(MainPage)}");
            await navigationService.GoBackAsync();
        }
        */
    }
}
