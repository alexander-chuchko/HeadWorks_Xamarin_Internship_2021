using Prism;
using Prism.Ioc;
using Prism.Unity;
using ProfileBook.Service;
using ProfileBook.Service.CameraAndImage;
using ProfileBook.Service.DatabaseСonverter;
using ProfileBook.Service.UserDialog;
using ProfileBook.Services;
using ProfileBook.Services.Interface;
using ProfileBook.View;
using ProfileBook.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfileBook
{
    public partial class App : PrismApplication
    { 
        public App():this(null)
        {
            //MainPage = new ProfileBook.MainPage();  ///new NavigationPage(new ProfileBook.MainPage()); // ProfileBook.View.SignUpView(); // MainPage();
        }
        public App(IPlatformInitializer initializer):base(initializer)
        {
            
        }
        protected override void OnStart()
        {

        }
        protected override void OnSleep()
        {

        }
        protected override void OnResume()
        {

        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<IAuthenticationService>(Container.Resolve<AuthenticationService>());
            containerRegistry.RegisterInstance<IDialog>(Container.Resolve<Dialog>());
            containerRegistry.RegisterInstance<ICameraAndImage>(Container.Resolve<СameraAndImage>());

            //Navigation
            //регистрируем страницу навигации
            containerRegistry.RegisterForNavigation<NavigationPage>();
            //регистрируем страницы в контейнере
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUp, SingUpViewModel>();
            containerRegistry.RegisterForNavigation<MainList, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfilePage, AddEditProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<Settings, SettingsViewModel>();
           
        }
        protected override async void OnInitialized()
        {
            InitializeComponent();
            //Кладем первую страницу в стек навигации
            var result=await NavigationService.NavigateAsync("NavigationPage/MainPage");
            //var result = await NavigationService.NavigateAsync($"{nameof(MainPage)}"));
            //var result = await NavigationService.NavigateAsync(($"{ nameof(SignUp)}"));
            //var result = await NavigationService.NavigateAsync(($"{nameof(NavigationPage)}/{ nameof(MainList)}"));
            //var result = await NavigationService.NavigateAsync(($"{nameof(NavigationPage)}/{ nameof(AddEditProfilePage)}"));
            //var result = await NavigationService.NavigateAsync(($"{ nameof(NavigationPage)}/{ nameof(MainList)}"));
            if (!result.Success)
            {
                System.Diagnostics.Debugger.Break();
            }
        }
    }
}