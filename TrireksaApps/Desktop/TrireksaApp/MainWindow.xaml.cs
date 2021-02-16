using FirstFloor.ModernUI.Windows.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
using TrireksaApp.Common;

namespace TrireksaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        readonly MainWindowVM viewmodel;
        public MainWindow()
        {
            InitializeComponent();
            viewmodel = new MainWindowVM();
            this.DataContext = viewmodel;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
          viewmodel.SignalClient.ConnectAsync();
          _=CheckUpdated();
        }

        private async Task CheckUpdated()
        {
            try
            {
                var thisVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                using (var client = new Client())
                {
                    var response = await client.ClientContext.GetAsync("api/appversion/last");
                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<ModelsShared.Models.AppVersion>(response.Content.ReadAsStringAsync().Result);
                        if (result != null)
                        {
                            if(result.Version != thisVersion)
                            {
                                ShowMessage($"Terdapat Update Versi Baru ({result.Version}) !");
                            }
                        }
                        else
                        {

                            throw new SystemException("User Or Password Invalid !..");
                        }

                    }
                    else
                    {
                        throw new SystemException("You Not Connect to Server");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void ShowMessage(string message)
        {
            ModernDialog.ShowMessage(message, "Info", MessageBoxButton.OK, this);
        }

        internal void ShowMessageError(string message)
        {
            ModernDialog.ShowMessage(message, "Error", MessageBoxButton.OK, this);
        }

        internal MessageBoxResult MessageAsk(string message)
        {
            return ModernDialog.ShowMessage(message, "Ask", MessageBoxButton.YesNo);
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            var dialog = ModernDialog.ShowMessage("Yakin Keluar Dari Aplikasi ?", "Keluar", MessageBoxButton.YesNo);
            if (dialog != MessageBoxResult.Yes)
                e.Cancel = true;
        }
      
    }
}