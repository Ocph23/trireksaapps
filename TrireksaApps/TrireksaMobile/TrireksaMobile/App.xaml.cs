using Accounts;
using System;
using TrireksaMobile.Services;
using TrireksaMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrireksaMobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<IUserServices, UserService>();
            DependencyService.Register<IDashboardService, DashboardService>();
            Load();
        }

        private void Load()
        {

            MainPage = new Views.LoginPage();

            if (Account.UserIsLogin)
            {
                MainPage = new AppShell();
                //if (await Account.UserInRole("Administrator"))
                //{
                //}
                //else if (await Account.UserInRole("Sales"))
                //{
                //    MainPage = new SalesShell();
                //}
                //else
                //{
                //    MainPage = new AppShell();
                //}
            }
            else
            {
                MainPage = new Views.LoginPage();
            }
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
    }
}
