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
    public class CityCollection
    {
        public event RefreshComplete RefreshCompleted;
        private readonly Client client = new Client("city");
        private SignalRClient signalRClient;

        public ObservableCollection<ModelsShared.Models.City> Source { get; set; }
        public CollectionView SourceView { get; set; }

        public ModelsShared.Models.City SelectedItem { get; set; }

        public CityCollection()
        {
            Source = new ObservableCollection<City>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            Refresh();

        }

        private async void InitAsync()
        {
            Source.Clear();
            var result = await client.GetAsync<List<City>>("");
            if (result != null)
            {
                foreach (var item in result)
                {
                    Source.Add(item);
                    SourceView.Refresh();
                }
            }
            signalRClient = ResourcesBase.GetSignalClient();
            signalRClient.OnAddCity += SignalRClient_OnAddCity;
            RefreshCompleted?.Invoke();
        }

        public Task Refresh()
        {
            InitAsync();
            return Task.FromResult(0);
        }

        private async void SignalRClient_OnAddCity(object sender)
        {
            await Task.Delay(2000);
            var result = JsonConvert.DeserializeObject<City>(sender.ToString());

            if (result != null)
            {
                var data = Source.Where(O => O.Id == result.Id).FirstOrDefault();
                if (data == null)
                {
                    Source.Add(result);
                    SourceView.Refresh();
                }

            }
        }

        internal Task<City> Post(City city)
        {

          return client.PostAsync<City>("",city);
            
        }

        internal async Task<bool> Update(int id, City city)
        {
            var res = await client.PutAsync<City>("",id, city);
            if(res!=default(City))
            {
                var source = Source.Where(O => O.Id == city.Id).FirstOrDefault();
                if(source!=null)
                {
                    source.CityCode = res.CityCode;
                    source.CityName = res.CityName;
                    source.Province = res.Province;
                    source.Regency = res.Regency;
                    return true;
                }
            }

            return false;
        }

        internal async Task<bool> Delete(int id)
        {
            var item = Source.Where(O => O.Id == id).FirstOrDefault();
            var istrue= await client.Delete<bool>("",id);
            if(istrue)
            {
                Source.Remove(item);
                SourceView.Refresh();
                return true;
            }

            return istrue;

        }
    }
}
