using Accounts;
using System;
using System.Threading.Tasks;
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
            DependencyService.Register<IPenjualanService, PenjualanService>();
            _ = Load();
        }

        private async Task Load()
        {

            MainPage = new Views.LoginPage();

            if (Account.UserIsLogin)
            {
                MainPage = new AppShell();

                if (await Account.UserInRole("Operational"))
                {
                    MainPage = new OperationalShell();
                }
                else
                {
                    MainPage = new AppShell();
                }
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
