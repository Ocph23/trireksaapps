using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrireksaMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvoiceDetailView : ContentPage
    {
        public InvoiceDetailView()
        {
            InitializeComponent();
        }
    }


    public class InvoiceDetailViewModel : BaseViewModel
    {
        public InvoiceDetailViewModel(List<Invoice> data, string title)
        {
            Source = new ObservableCollection<Invoice>(data);
            Title = title;
        }


        public ObservableCollection<Invoice> Source { get; }
    }
}