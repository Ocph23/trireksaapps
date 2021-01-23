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
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List : UserControl
    {
        private readonly InvoiceListVM viewmodel;

        public List()
        {
            InitializeComponent();
            this.viewmodel = new InvoiceListVM();
            this.DataContext = viewmodel;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // var data = ((ListView)sender).SelectedItem;
     //      
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var data = (ModelsShared.Models.Invoice)list.SelectedItem;
            if(data!=null)
                NavigationCommands.GoToPage.Execute($"/Contents/Invoice/Create.xaml#{data.Id}", this);
        }
    }
}
