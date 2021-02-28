using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Service;
using ProfileBook.View;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    class SingUpViewModel: BindableBase
    {
        public DelegateCommand GoBackCommand { get; set; }
        private readonly INavigationService navigationService;
        private IAuthenticationService authenticationService;
        public ICommand commandVerefication => new Command(ExecuteVereficationLoginAndPasswordAndConfirmPassword);
        private string titlePage;
        private string login;
        private string password;
        private string confirmPasword;
        public SingUpViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            this.navigationService = navigationService;
            GoBackCommand = new DelegateCommand(ExecuteNavigateCommand);
            this.authenticationService = authenticationService;
            TitlePage = ($"{ nameof(SignUp)}");
        }
        async void ExecuteNavigateCommand()
        {
            await navigationService.GoBackAsync();
        }
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
        public string ConfirmPasword
        {
            get => confirmPasword;
            set => SetProperty(ref confirmPasword, value);
        }
        public async void ExecuteVereficationLoginAndPasswordAndConfirmPassword()
        {
            var result = this.authenticationService.IsCheckLoginAndPasswordAndConfirmPassword(Login, Password, ConfirmPasword);
            if (result)
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
    }
}
