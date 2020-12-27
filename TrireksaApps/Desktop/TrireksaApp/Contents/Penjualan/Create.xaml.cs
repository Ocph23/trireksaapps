using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TrireksaApp.Contents.Penjualan
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : UserControl
    {
        private readonly PenjualanCreateVM viewmodel;

        public Create()
        {
            InitializeComponent();
            viewmodel= new PenjualanCreateVM();
            this.DataContext = viewmodel;
            Loaded += Create_Loaded;
        }

        private void Create_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void TypeOfWeightCgange(object sender, SelectionChangedEventArgs e)
        {
            var vm = (Contents.Penjualan.PenjualanCreateVM)this.DataContext;
            var cmb = (ComboBox)sender;
            vm.SetWeight(cmb.SelectedItem);
        }

        private void AddShiper(object sender, KeyEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
                cmb.IsDropDownOpen = true;
            if (e.Key== Key.Enter && viewmodel.STTModel!=null && viewmodel.STTModel.Shiper==null)
            {
                
                var form = new Contents.Customer.Create();
                var vm = form.DataContext as Contents.Customer.CustomerCreateVM;
                vm.Name = cmb.Text;
              var dlg = new ModernDialog { MinWidth=800, MinHeight=500, Content = form, Title="Add Shiper" };
                dlg.ShowDialog();
            }
       
        }

        private void AddReciver(object sender, KeyEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            cmb.IsDropDownOpen = true;
            if (e.Key == Key.Enter && viewmodel.STTModel != null && viewmodel.STTModel.Reciver == null)
            {
               
                var form = new Contents.Customer.Create();
                var vm = form.DataContext as Contents.Customer.CustomerCreateVM;
                vm.Name = cmb.Text;
                var dlg = new ModernDialog { MinWidth = 800, MinHeight = 500, Content = form, Title = "Add Reciver" };
                dlg.ShowDialog();
            }
        }

        private void SearchPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox tb = (TextBox)sender;
                string spb = tb.Text;
                tb.Text = spb;
                viewmodel.Search.Execute(tb.Text);
                
                via.Focus();
            }
                   
        }

        private void ComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            cmb.IsDropDownOpen = true;
        }
     
    }
}
