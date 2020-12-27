using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Penjualan
{
    public sealed class CustomerControl : ComboBox
    {
        public MainWindowVM MainVM { get; }
        public ObservableCollection<ModelsShared.Models.Customer> Source { get; }
        public CollectionView SourceView { get; }

        public CustomerControl()
        {
            IsDropDownOpen = true;
            IsEditable = true;
            IsTextSearchEnabled = true;
            DisplayMemberPath = "Name";
            SelectedValuePath = "Id";

            this.MainVM = ResourcesBase.GetMainWindowViewModel();
            this.Source = new ObservableCollection<ModelsShared.Models.Customer>(MainVM.CustomerCollection.Source);
            this.SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            this.MainVM.CustomerCollection.Source.CollectionChanged += Source_CollectionChanged;
            this.ItemsSource = SourceView;
            PreviewTextInput += CustomerControl_PreviewTextInput;
            SourceView.Filter = SourceViewFilter;
            SourceView.Refresh();
        }

        private void CustomerControl_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            SourceView.Refresh();
            if (this.SourceView.Count <= 0)
                Refresh();
        }

        private void Source_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var customers = (IEnumerable<ModelsShared.Models.Customer>)sender;
            if (customers != null )
            {
                Source.Clear();
                foreach (var item in customers)
                {
                    Source.Add(item);
                }
            }
            SourceView.Refresh();
        }

        private bool SourceViewFilter(object x)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return true;
            }

            var obj = (ModelsShared.Models.Customer)x;

            if (obj != null)
            {
                string scr = Text.ToUpper().Trim();
                var data = obj.Name.ToUpper();
                return data.Contains(scr);
            }
            return true;
        }

       

        private async void Refresh()
        {
            await Task.Delay(20);
            if(MainVM.CustomerCollection.Source.Count != Source.Count)
            {
                Source.Clear();
                foreach (var item in MainVM.CustomerCollection.Source)
                {
                    Source.Add(item);
                }
            }
            SourceView.Refresh();
        }

     
      
        public void Clear()
        {
            SelectedItem = null;
            SourceView.Refresh();
        }




    }
}
