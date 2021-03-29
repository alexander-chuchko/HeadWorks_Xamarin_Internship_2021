using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Model;
using ProfileBook.Service;
using ProfileBook.Services.Interface;
using ProfileBook.View;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class MainPageViewModel:BindableBase, INavigatedAware, IInitializeAsync
    {
        //fields
        private string _titlePage;
        private string _login;
        private string _password;
        private bool _isEnabled;
        private int id;

        private IAuthenticationService authenticationService;
        private INavigationService navigationService;
        private readonly IRepository repository;
        private ObservableCollection<UserModel> userList;
        //properties
        public ICommand NavigationToSingUp { get; set; }
        public ICommand SignInCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IRepository repository)
        {
            IsEnabled = false;
            Login = string.Empty;
            Password = string.Empty;
            this.repository = repository;
            this.navigationService = navigationService;
            this.authenticationService = authenticationService;
            SignInCommand = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => IsEnabled);//For verefication and navigation
            NavigationToSingUp = new DelegateCommand(ExecuteNavigateToSignUp); //NavigationToSingUp for navigation to page SignUp(Tab Label)
            TitlePage = ($"{ nameof(MainPage)}");
        }
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
        public ObservableCollection<UserModel> UserList
        {
            get => userList;
            set => SetProperty(ref userList, value);
        }
        //Go to the main page of the application
        public async void ExecuteNavigateToListView()
        {
            //get id the logged-in user
            id = authenticationService.Id;
            var parametr = new NavigationParameters();
            parametr.Add("CurrentId", id);
            await navigationService.NavigateAsync(($"/{ nameof(NavigationPage)}/{ nameof(MainList)}"), parametr);
        }
        public async void ExecuteNavigateToSignUp()
        {
            await navigationService.NavigateAsync(($"{ nameof(SignUp)}"));
        }
        public void Execute()
        {
            if (Validation.IsValidatedLoginAndPassword(Login, Password) && authenticationService.IsRelevantLoginAndPassword(UserList, Login, Password))
            {
                ExecuteNavigateToListView();
            }
            else
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
            Login = parameters.GetValue<string>("Currenlogin");
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
        public async Task InitializeAsync(INavigationParameters parameters)
        {
            //Get a profileList from the repository
            var userList = await repository.GetAllAsync<UserModel>();
            //Set a profilelist in the ProfileList
            UserList = new ObservableCollection<UserModel>(userList);
        }
    }
}
