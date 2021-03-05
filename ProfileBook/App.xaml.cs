using Prism;
using Prism.Ioc;
using Prism.Unity;
using ProfileBook.Service;
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

            //Navigation
            //регистрируем страницу навигации
            containerRegistry.RegisterForNavigation<NavigationPage>();
            //регистрируем страницу в контейнере
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUp, SingUpViewModel>();
            containerRegistry.RegisterForNavigation<MainList, MainListViewModel>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            //Кладем первую страницу в стек навигации
           var result=await NavigationService.NavigateAsync("NavigationPage/MainPage");
            //var result = await NavigationService.NavigateAsync($"{nameof(MainPage)}"));
            //var result = await NavigationService.NavigateAsync(($"{ nameof(SignUp)}"));
            if (!result.Success)
            {
                System.Diagnostics.Debugger.Break();
            }
        }
    }
}