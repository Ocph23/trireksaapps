using ModelsShared;
using System.Threading.Tasks;
using System.Windows.Data;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Penjualan
{
    public  class CustomerControl      :BaseNotify
    {
        private string _searchText;
        private string _toolTip;

        public MainWindowVM MainVM { get; }
        public CollectionView SourceView { get; }

        public CustomerControl()
        {
            this.MainVM = ResourcesBase.GetMainWindowViewModel();
            this.SourceView = (CollectionView)CollectionViewSource.GetDefaultView(MainVM.CustomerCollection.Source);

            SourceView.Filter = sourceViewFilter;
        }



        private bool sourceViewFilter(object x)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                return false;
            }

            var obj = (ModelsShared.Models.Customer)x;


            if (obj != null && !string.IsNullOrEmpty(SearchText))
            {
                string scr = SearchText.ToUpper().Trim();
                var data = obj.Name.ToUpper();
                return data.Contains(scr);
            }
            return true;
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                SetProperty(ref _searchText, value);
                Refresh();
            }
        }

        private async void Refresh()
        {
            await Task.Delay(20);
            SourceView.Refresh();
        }

        public string ToolTip
        {
            get { return _toolTip; }
            set
            {
                SetProperty(ref _toolTip, value);
                SourceView.Refresh();
            }
        }


        private ModelsShared.Models.Customer customer;

        public ModelsShared.Models.Customer SelectedItem
        {
            get { return customer; }
            set { SetProperty(ref customer , value); }
        }



    }
}
