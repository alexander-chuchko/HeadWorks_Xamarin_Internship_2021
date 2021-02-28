using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Service;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class MainPageViewModel:BindableBase
    {
        private string titlePage;
        private string login;
        private string password;

        private IAuthenticationService authenticationService;

        private INavigationService navigationService;
        //Properties
        public DelegateCommand NavigationToSingUp { get; set; }
        public DelegateCommand SignInCommand { get; set; }
        public ICommand commandVerefication => new Command(ExecuteVereficationLoginAndPassword);  
        public string TitlePage
        {
            get => titlePage;
            set => SetProperty(ref titlePage, value);
        }
        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        public MainPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            this.navigationService = navigationService;
            this.authenticationService = authenticationService;
            NavigationToSingUp = new DelegateCommand(ExecuteNavigateCommand);
            //SignInCommand = new DelegateCommand(this.authenticationService.CheckLogin(Login));
            TitlePage = ($"{ nameof(MainPage)}");
        }
        public async void ExecuteVereficationLoginAndPassword()
        {
            var result=this.authenticationService.IsCheckLoginAndPassword(Login, Password);
            if(!result)
            {
                //выполняем проверку в БД
                
                //Происходит навигация на главную страницу
                await navigationService.NavigateAsync("MainList");
            }
            else
            {
                //Вызываем сообщение об ошибке
            }
        }
        public async void ExecuteNavigateCommand()
        {
            await navigationService.NavigateAsync("SignUp");
        }
    }
}
