using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using ModelsShared;
using ModelsShared.Models;
using ModelsShared.ReportModels;
using TrireksaApp.Common;
using TrireksaApp.Models;

namespace TrireksaApp.CollectionsBase
{
   
    public class PenjualanCollection:BaseCollection
    {

        public event RefreshComplete RefreshCompleted;
        private Client client = new Client("Penjualans");

        public ObservableCollection<Penjualan> Source { get; set; }

        public PenjualanCollection()
        {
            Source = new ObservableCollection<Penjualan>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
        }

        internal async Task<List<PenjualanReportModel>> GetPenjualanFromTo(DateTime start, DateTime end)
        {
            var url = string.Format("GetPenjualanFromTo?start={0}-{1}-{2}&ended={3}-{4}-{5}",start.Year,start.Month,start.Day,
                end.Year,end.Month,end.Day);
            var result = await client.GetAsync<List<PenjualanReportModel>>(url);
            return result;
        }

        private async void InitAsync()
        {
            Source.Clear();
            var url = string.Format("{0}-{1}-{2}/{3}-{4}-{5}", StartDate.Year, StartDate.Month, StartDate.Day,
              EndDate.Year, EndDate.Month, EndDate.Day);
            var result = await client.GetAsync<List<Penjualan>>(url);
            if (result != null)
            {
                foreach (var item in result)
                {
                    Source.Add(item);
                    await Task.Delay(10);
                    
                }
            }
            SourceView.Refresh();
            RefreshCompleted?.Invoke();
        }

        public Task Refresh()
        {
            InitAsync();
            return Task.FromResult(0);
        }


        public async Task<Penjualan> GetItemBySTT(int id)
        {
            var result = this.Source.Where(O => O.STT == id).FirstOrDefault();
            if (result == null)
            {
                result = await client.GetAsync<Penjualan>("", id);
                if (result != null)
                {

                    this.Source.Add(result);
                
                }
            }
            SourceView.Refresh();
            return result;
        }

        public async Task<Penjualan> GetItemById(int id)
        {
            var result = this.Source.Where(O => O.Id == id).FirstOrDefault();
            if (result == null)
            {
                result = await client.GetAsync<Penjualan>("GetById", id);
                if (result != null)
                {

                    this.Source.Add(result);

                }
            }
            SourceView.Refresh();
            return result;
        }


        public async Task<Penjualan> Add(Penjualan item)
        {
            var result = await client.PostAsync<Penjualan>("", item);
            if(result!=null)
            {
                Source.Add(result);
                SourceView.Refresh();
            }
            return result;
        }

        public CollectionView SourceView { get; set; }

        internal async Task<ModelsShared.Models.Colly> RemoveItem(ModelsShared.Models.Colly selectedItem)
        {
            using (var client = new Client("Collies"))
            {
                return await client.Delete<ModelsShared.Models.Colly>("", selectedItem.Id);
            }
        }

        public Penjualan SelectedItem { get; set; }

        internal Task<List<Penjualan>> GetPenjualanNotPaid(int id)
        {
           return client.GetAsync<List<Penjualan>>("GetPenjualanNotPaid", id);
        }

        internal async Task<bool> DeletePhoto(ModelsShared.Photo selectedPhoto)
        {
            var clientPhoto = new Client("Photos");
            var res = await clientPhoto.Delete<bool>("DeletePhoto", selectedPhoto.Id);
            return res;
        }

        internal Task<List<Penjualan>> GetByParameter(Manifestoutgoing manifestoutgoing)
        {
            var uri = "GetByParameter?agentId=" + manifestoutgoing.AgentId + "&type=" + manifestoutgoing.PortType;
            return client.GetAsync<List<Penjualan>>(uri);
        }

        internal Task<int> NewSTT()
        {
            return  client.GetAsync<int>("NewSTT");
        }

        internal async Task<List<ModelsShared.Photo>> GetPhotoByPenjualanId(int id)
        {
            var clientPhoto = new Client("Photos");
            var res= await clientPhoto.GetAsync<List<ModelsShared.Photo>>("GetPhotosByPenjualanId", id);
            return res;
        }

        internal Task<bool> IsSended(int id)
        {
            return client.GetAsync<bool>("IsSended");
        }

        internal async Task<bool> UpdateDeliveryStatus(Deliverystatus deliveryStatus)
        {
            var res = await client.PutAsync<Deliverystatus>("UpdateDeliveryStatus", deliveryStatus.Id, deliveryStatus);
            if (res != default(Deliverystatus))
            {
                return true;
            }
            else
                return false;
         
        }

        internal async Task<ModelsShared.Photo> AddNewPhoto(ModelsShared.Photo ph)
        {
            var clientPhoto = new Client("Photos");
            var res = await clientPhoto.PostAsync<ModelsShared.Photo>("AddNewPhoto", ph);
            
            return res;
        }

        internal async Task<bool> Update(int id, Penjualan penj)
        {
            var item =await client.PutAsync<Penjualan>("",id, penj);
            var data = Source.Where(O => O.Id == id).FirstOrDefault();
            if (item!=default(Penjualan) && data!=null)
            {
                data.CustomerIsPay = item.CustomerIsPay;
                data.Details = item.Details;
                data.Etc = item.Etc;
                data.FromCity = item.FromCity;
                data.ToCity = item.ToCity;
                data.PackingCosts = item.PackingCosts;
                data.PayType = item.PayType;
                data.PortType = item.PortType;
                data.Price = item.Price;
                data.ReciverID = item.ReciverID;
                data.ShiperID = item.ShiperID;
                data.STT = item.STT;
                data.Tax = item.Tax;
                data.Total = item.Total;
                data.TypeOfWeight = item.TypeOfWeight;
                data.UserID = item.UserID;
                data.Content = item.Content;
                data.DoNumber = item.DoNumber;
                data.Id = item.Id;
                data.ChangeDate = item.ChangeDate;
                data.Note = item.Note;
                SourceView.Refresh();
                return true;
            }

            return false;
        }

        internal async Task<byte[]> GetPictureById(int id)
        {
            var clientPhoto = new Client("Photos");
            var res = await clientPhoto.GetAsync<byte[]>("GetPictureById", id);
            return res;
        }
    }
}
