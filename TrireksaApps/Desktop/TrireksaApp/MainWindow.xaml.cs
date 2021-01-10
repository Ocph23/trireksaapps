using FirstFloor.ModernUI.Windows.Controls;
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

    }
}