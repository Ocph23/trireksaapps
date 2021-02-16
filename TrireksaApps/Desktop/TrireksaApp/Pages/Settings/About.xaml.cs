using System;
using System.Reflection;
using System.Windows.Controls;

namespace TrireksaApp.Pages.Settings
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
            this.copyRight.Content = $"@Ocph23  2016 - {DateTime.Now.Year}";
            this.version.Content= "Version : "+ Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
