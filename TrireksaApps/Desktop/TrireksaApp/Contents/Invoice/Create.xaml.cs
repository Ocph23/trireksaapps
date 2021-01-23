using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrireksaApp.Contents.Invoice
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : UserControl , IContent
    {
        private readonly InvoiceCreateVM viewmodel;

        public Create()
        {
            InitializeComponent();
            this.viewmodel = new Contents.Invoice.InvoiceCreateVM();
            this.DataContext = viewmodel;
        }


        public async void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {

            if (e.Fragment != null)
            {
                var data = Convert.ToInt32(e.Fragment);
                if (data > 0)
                    await viewmodel.SetInvoice(data);
            }
            else
            {
                await viewmodel.SetInvoice(0);
            }
           
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
          
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
           //var paramss= e.Source.OriginalString.Split('#')[1];
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
       
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationCommands.GoToPage.Execute($"/Contents/Invoice/Create.xaml", this);
        }
    }
}
