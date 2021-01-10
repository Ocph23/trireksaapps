using System.ComponentModel;
using TrireksaMobile.ViewModels;
using Xamarin.Forms;

namespace TrireksaMobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}