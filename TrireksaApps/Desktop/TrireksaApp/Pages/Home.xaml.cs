using ModelsShared;
using System.Windows.Controls;
using TrireksaApp.Common;

namespace TrireksaApp.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        private readonly HomeMainViewModel vm;

        public Home()
        {
            InitializeComponent();
            vm= new HomeMainViewModel();
            this.DataContext = vm;
            ResourcesBase.HomeVM = vm;
        }
    }

    public class HomeMainViewModel:BaseNotify
    {
        public HomeMainViewModel()
        {
            Inittial();
        }

        private async void Inittial()
        {
            var clien = new Client("Test");
            var res =  await clien.GetAsync<string>("GetLocalIpAddress");
            IPAdress = res;
        }

        
        private string ip;
        private string _barMessage;

        public string IPAdress
        {
            get { return ip; }
            set {
               SetProperty(ref ip , value);
            }
        }

        public string BarMessage
        {
            get { return _barMessage; }
            set
            {
                SetProperty(ref _barMessage, value);
            }
        }

    }



}
