﻿using Prism.Ioc;
using Prism.Unity;
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
        public App()
        {
            //MainPage = new ProfileBook.MainPage();  ///new NavigationPage(new ProfileBook.MainPage()); // ProfileBook.View.SignUpView(); // MainPage();
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
            //Navigation
            //регистрируем страницу навигации
            containerRegistry.RegisterForNavigation<NavigationPage>();
            //регистрируем страницу в контейнере
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUp, SingUpViewModel>();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            //Кладем первую страницу в стек навигации
            NavigationService.NavigateAsync($"{nameof(MainPage)}");
        }
    }
}