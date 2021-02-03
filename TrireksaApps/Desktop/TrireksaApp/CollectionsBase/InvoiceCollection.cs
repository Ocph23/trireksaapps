using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModelsShared.Models;
using System.Collections.ObjectModel;
using System.Windows.Data;
using TrireksaApp.Common;

namespace TrireksaApp.CollectionsBase
{
    public class InvoiceCollection :BaseCollection
    {

        private readonly Client client = new Client("Invoices");
        private Invoice _selected;
        public event RefreshComplete RefreshCompleted;


        public Invoice SelectedItem
        {
            get { return _selected; }
            set { _selected = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public ObservableCollection<Invoice> Source { get; set; }
        public CollectionView SourceView { get; set; }

        public InvoiceCollection()
        {
            Source = new ObservableCollection<Invoice>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            SourceView.SortDescriptions.Add(new System.ComponentModel.SortDescription { PropertyName = "Number", Direction = System.ComponentModel.ListSortDirection.Descending });
        }

        private async void InitAsync()
        {
            Source.Clear();
            var url = string.Format("{0}-{1}-{2}/{3}-{4}-{5}", StartDate.Date.Year,StartDate.Date.Month,StartDate.Day,
                EndDate.Year, EndDate.Month, EndDate.Day) ;
            var result = await client.GetAsync<List<Invoice>>(url);
            if(result!=null)
            {
                foreach (var item in result)
                {
                    Source.Add(item);
                    SourceView.Refresh();
                }
            }
            RefreshCompleted?.Invoke();
        }

        public async Task<bool> Add(Invoice item)
        {
            var x = await client.PostAsync<Invoice>("",item);
            if (x != null)
            {
                this.Source.Add(x);
                SourceView.Refresh();
                this.SelectedItem = x;
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<Invoice> GetItemById(int Id)
        {
            var result = await client.GetAsync<Invoice>("", Id);
            return result;
        }

        internal async Task<bool> UpdateInvoiceStatusAction(int id, Invoice item)
        {
            var res= await client.PutAsync<Invoice>("UpdateInvoiceStatusAction",id, item);
            if (res != default(Invoice))
                return true;
            else
                return false;
        }

        internal async Task<bool> UpdateDeliveryDataAction(int id, Invoice item)
        {
            var res=await client.PutAsync<Invoice>("UpdateDeliveryDataAction", id, item);
            if (res != default(Invoice))
                return true;
            else
                return false;
        }

        internal async Task<Invoice> GetInvoiceForPenjualanInfo(int id)
        {
            var res= await client.GetAsync<Invoice>("GetInvoiceForPenjualanInfo", id);
            return res;
        }

        public Task RefreshAction()
        {
            InitAsync();
            return Task.FromResult(0);
        }

        internal async Task<bool> Update(Invoice item)
        {
            var x = await client.PutAsync<Invoice>($"",item.Id, item);
            if (x != null)
            {
                this.SelectedItem = x;
                SourceView.Refresh();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
