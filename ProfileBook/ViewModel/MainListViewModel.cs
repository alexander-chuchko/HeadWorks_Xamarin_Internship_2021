using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Model;
using ProfileBook.Services.Interface;
using ProfileBook.View;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class  MainListViewModel: BindableBase, IInitializeAsync, INavigatedAware
    {
        private string _titlePage;
        private int _id;
        private readonly INavigationService navigationService;
        private readonly IRepository repository;
        private ObservableCollection<ProfileModel> profilelList;
        private ProfileModel selectedItem;

        public ICommand NavigationToSingIn { get; set; }
        public ICommand NavigationToSettingsView { get; set; }
        public ICommand NavigationToAddEditProfilePage { get; set; }
        public ICommand OnDeleteItem { get; set; }
        public MainListViewModel(INavigationService navigationService, IRepository repository)
        {
            this.navigationService = navigationService;
            this.repository = repository;
            TitlePage = ($"{ nameof(MainList)}");
            NavigationToSingIn = new DelegateCommand(ExecuteGoBack, CanExecute);
            NavigationToSettingsView = new DelegateCommand(ExecuteGoToSettingsPage, CanExecuteGoToSettingsPage);
            NavigationToAddEditProfilePage = new DelegateCommand(ExecuteGoToAddEditProfilePage, CanExecuteGoToAddEditProfilePage);
            OnDeleteItem = new Command(DeleteItem);
        }
        public ProfileModel SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }
        public string TitlePage
        {
            get => _titlePage;
            set => SetProperty(ref _titlePage, value);
        }
        public ObservableCollection<ProfileModel> ProfileList
        {
            get => profilelList;
            set => SetProperty(ref profilelList, value);
        }
        public async void ExecuteGoBack()
        {
            await navigationService.NavigateAsync($"/{ nameof(NavigationPage)}/{ nameof(MainPage)}");
        }
        public bool CanExecute()
        {
            return true;
        }
        public async void ExecuteGoToSettingsPage()
        {
            await navigationService.NavigateAsync($"{ nameof(Settings)}");
        }
        public bool CanExecuteGoToSettingsPage()
        {
            return true;
        }
        public async void ExecuteGoToAddEditProfilePage()
        {
            var parametr = new NavigationParameters();
            parametr.Add("CurrentId", _id);
            await navigationService.NavigateAsync(($"{ nameof(AddEditProfilePage)}"), parametr);
        }
        public bool CanExecuteGoToAddEditProfilePage()
        {
            return true;
        }
        public async Task InitializeAsync(INavigationParameters parameters)
        {
            if(SelectedItem!=null)
            {

            }
            //Get a profileList from the repository
            var userList = await repository.GetAllAsync<ProfileModel>();
            //Set a profilelist in the ProfileList
            ProfileList = new ObservableCollection<ProfileModel>(userList);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
           
        }
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _id = parameters.GetValue<int>("CurrentId");
            SelectedItem = parameters.GetValue<ProfileModel>("ProfileUser");
        }

        public async void AddProfile()
        {
            await repository.InsertAsync<ProfileModel>(SelectedItem);
        }

        public async void OnUpdateTap()
        {
            if (SelectedItem != null)
            {
                await repository.UpdateAsync(SelectedItem);
                SelectedItem.Name = "";
                SelectedItem.NickName = "";
                SelectedItem.Description = "";
                SelectedItem.ImageSource = "";
            }
        }
        public async void DeleteItem()
        { 
            if(SelectedItem!=null)
            {
                await repository.DeleteAsync(SelectedItem);
                ProfileList.Remove(SelectedItem);
            }
        }
    }
}
