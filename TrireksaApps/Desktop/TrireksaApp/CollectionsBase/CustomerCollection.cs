using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using ModelsShared.Models;
using Newtonsoft.Json;
using TrireksaApp.Common;

namespace TrireksaApp.CollectionsBase
{
    public class CustomerCollection
    {
        public ObservableCollection<Customer> Source { get; set; }
        public  CollectionView SourceView { get; set; }
        private readonly Client client = new Client("customers");
        public Customer SelectedItem { get; set; }
        private SignalRClient SignalRClient { get; set; }

        public CustomerCollection()
        {
            Source = new ObservableCollection<ModelsShared.Models.Customer>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            InitAsync();

        }


        public void Refresh()
        {
            InitAsync();
        }

        private async void SignalRClient_OnUpdateCUstomer(object sender)
        {
            await Task.Delay(2000);
            var result = JsonConvert.DeserializeObject<Customer>(sender.ToString());
          
            if(result!=null)
            {
                var data = Source.Where(O => O.Id == result.Id).FirstOrDefault();
                if(data==null)
                {
                    Source.Add(result);
                    SourceView.Refresh();
                }
                   
            }
        }
       
        private async void InitAsync()
        {

            Source.Clear();
            var result = await client.GetAsync<List<Customer>>("");
            if(result!=null)
            {
                foreach (var item in result)
                {
                    Source.Add(item);
                    SourceView.Refresh();
                }
            }

            SignalRClient = ResourcesBase.GetSignalClient();
            SignalRClient.OnAddCustomer+= SignalRClient_OnUpdateCUstomer;


        }

        public Customer GetItemById(int Id)
        {
            return Source.Where(O => O.Id == Id).FirstOrDefault();
        }

        public async Task<bool> Add(Customer item)
        {
            var res = await client.PostAsync<Customer>("",item);
            if (res != null)
            {
                this.Source.Add(res);
                SourceView.Refresh();
                return true;
            }
            else
                return false;
        }

        internal async Task<bool> RegisterCustomer(Customer selectedItem)
        {
            var respon = await client.PostAsync<Customer>("RegisterCustomer", selectedItem);
            if(respon!=null)
                return true;
            return false;
        }

        internal async Task<bool> Update(int id, Customer customer)
        {
            var newitem = await client.PutAsync<Customer>("",id, customer);
            if (newitem!=default(Customer))
            {
                var item = Source.Where(O => O.Id == id).FirstOrDefault();
                if (item != null)
                {
                    item.Address = newitem.Address;
                    item.ContactName = newitem.ContactName;
                    item.CustomerType = newitem.CustomerType;
                    item.Email = newitem.Email;
                    item.Handphone = newitem.Handphone;
                    item.Name = newitem.Name;
                    item.Phone1 = newitem.Phone1;
                    item.Phone2 = newitem.Phone2;
                    item.CityID = newitem.CityID;
                    return true;
                }
                SourceView.Refresh();
            }
            return false;
        }

        internal async Task<bool> Delete(int id)
        {
            bool result = await client.Delete<bool>("",id);
            if (result)
            {
                Source.Remove(SelectedItem);
                SourceView.Refresh();
            }

            return result;
          
        }
    }
}
