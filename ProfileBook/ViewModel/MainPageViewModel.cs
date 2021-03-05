using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Service;
using ProfileBook.View;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class MainPageViewModel:BindableBase, INavigatedAware
    {
        private string _titlePage;
        private string _login;
        private string _password;
        private bool _isEnabled;

        private IAuthenticationService authenticationService;
        private INavigationService navigationService;
        //Properties
        public ICommand NavigationToSingUp { get; set; }
        public ICommand SignInCommand { get; set; }
        
        public string TitlePage
        {
            get => _titlePage;
            set => SetProperty(ref _titlePage, value);
        }
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }
        public MainPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            Login = string.Empty;
            Password = string.Empty;

            this.navigationService = navigationService;
            this.authenticationService = authenticationService;
            SignInCommand = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => IsEnabled);
            NavigationToSingUp = new DelegateCommand(ExecuteNavigateCommand);
            TitlePage = ($"{ nameof(MainPage)}");
        }
        public async void ExecuteVereficationLoginAndPassword()
        {
            //var result=this.authenticationService.IsCheckLoginAndPassword(Login, Password);
            //if(!result)
            if(true)
            {
                //выполняем проверку в БД
                //Происходит навигация на главную страницу
                await navigationService.NavigateAsync(($"{ nameof(SignUp)}"));
            }
            else
            {
                //Вызываем сообщение об ошибке
            }
        }
        //Переходим на страницу SignUp
        public async void ExecuteNavigateCommand()
        {
            await navigationService.NavigateAsync(($"{ nameof(SignUp)}"));
        }
        public void Execute()
        {
            var result = Helper.IsCheckLoginAndPassword(Login, Password);
            if (!result)
            {
                Login = string.Empty;
                Password = string.Empty;
            }
        }
        public bool CanExecute()
        {
            return IsEnabled;
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            //Login = parameters.ToString();
            Login = parameters.GetValue<string>("Currenlogin");
            //Login = parameters["Currenlogin"].ToString();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
        /*
async void ExecuteNav()
{
await navigationService.NavigateAsync("SignUp");
}*/
    }
}
