using ModelsShared.ReportModels;
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
    public partial class PenjualanDetailView : ContentPage
    {
        public PenjualanDetailView()
        {
            InitializeComponent();
        }
    }


    public class PenjualanDetailViewModel : BaseViewModel
    {
        public PenjualanDetailViewModel(List<PenjualanReportModel> data, string title)
        {
            Source = new ObservableCollection<PenjualanReportModel>(data);
            Title = title;
        }

        public ObservableCollection<PenjualanReportModel> Source { get; }
    }
}