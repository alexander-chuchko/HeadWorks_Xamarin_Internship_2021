using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Model;
using ProfileBook.Service;
using ProfileBook.Service.UserDialog;
using ProfileBook.Services.Interface;
using ProfileBook.View;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    class SingUpViewModel: BindableBase, IInitializeAsync
    {
        //fields of class
        private string _titlePage;
        private string _login;
        private string _password;
        private string _confirmPasword;
        private bool _isEnabled;

        public ICommand SignUpCommand { get; set; }
        public ICommand CommandGoBack { get; set; }
        private readonly IRepository repository; 
        private readonly INavigationService navigationService;
        private readonly IAuthenticationService authenticationService;
        private ObservableCollection<UserModel> userList;

        public SingUpViewModel(INavigationService navigationService, IRepository repository, IAuthenticationService authenticationService)
        {
            IsEnabled = false;
            Login = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            this.navigationService = navigationService;
            this.authenticationService = authenticationService;
            this.repository = repository;
            SignUpCommand = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => IsEnabled);
            CommandGoBack = new DelegateCommand(ExecuteGoBack);
            TitlePage = ($"{ nameof(SignUp)}");  
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
        public string ConfirmPassword
        {
            get => _confirmPasword;
            set => SetProperty(ref _confirmPasword, value);
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
        public void SignIn()
        {
            navigationService.NavigateAsync("MainList");
        }
        public async void ExecuteGoBack()
        {
            var parametr = new NavigationParameters();
            parametr.Add("Currenlogin", Login);
            await navigationService.NavigateAsync("/MainPage", parametr);
            //await navigationService.GoBackAsync(parametr);
        }
        public async void AddUserModel()
        {
            await repository.InsertAsync<UserModel>(new UserModel() { Login = Login, Password = Password });
        }
        public void Execute()
        {
            if (Validation.IsValidatedLoginAndPasswordAndConfirmPassword(Login, Password, ConfirmPassword) && authenticationService.IsLoginUniqe(UserList, Login))
            {
                //If the check is successful, then add the modelUser to the database
                AddUserModel();
                //и навигируемся на страницу
                ExecuteGoBack();
            }
            else
            {
                Login = string.Empty;
                Password = string.Empty;
                ConfirmPassword = string.Empty;
            }
        }
        public bool CanExecute()
        {
            return IsEnabled;
        }
        //Метод срабатывает при переходе на страницу View
        public async Task InitializeAsync(INavigationParameters parameters)
        {
            //Get a profileList from the repository
            var userList = await repository.GetAllAsync<UserModel>();
            //Set a profilelist in the ProfileList
            UserList = new ObservableCollection<UserModel>(userList);
        }
    }
}
