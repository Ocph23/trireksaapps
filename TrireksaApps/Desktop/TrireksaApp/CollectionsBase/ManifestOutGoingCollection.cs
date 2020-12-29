using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using ModelsShared.Models;
using TrireksaApp.Common;

namespace TrireksaApp.CollectionsBase
{
    public class ManifestOutGoingCollection:BaseCollection
    {
        public event RefreshComplete RefreshCompleted;
        private Client client = new Client("ManifestOutgoing");
        public ObservableCollection<ModelsShared.Models.Manifestoutgoing> Source { get; set; }
        public CollectionView SourceView { get; set; }


        public virtual Manifestoutgoing SelectedItem { get; set; }

        public ManifestOutGoingCollection()
        {
            Source = new ObservableCollection<ModelsShared.Models.Manifestoutgoing>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
        }

        public Task Refresh()
        {
            InitAsync();
            return Task.FromResult(0);
        }

        public async void InitAsync()
        {
            Source.Clear();
            var result = await client.GetAsync<List<Manifestoutgoing>>("");
            if (result != null)
            {
                var results = result.Where(O => O.CreatedDate >= StartDate && O.CreatedDate <= EndDate).ToList();
                foreach (var item in results)
                {
                    Source.Add(item);
                   
                }
            }
            SourceView.Refresh();
            RefreshCompleted?.Invoke();
        }

        public Task<Manifestoutgoing> GetItemById(int Id)
        {
            return client.GetAsync<Manifestoutgoing>("Get", Id);
        }

        public async Task<Manifestoutgoing> Add(Manifestoutgoing item)
        {
            var result = await client.PostAsync<Manifestoutgoing>("",item);
            if (result != null)
                Source.Add(result);
            return result;
        }

        internal async Task<bool> UpdateInformation(ManifestInformation information)
        {
            var result = await client.PostAsync<ManifestInformation>("UpdateInformation",information);
            if (result != default(ManifestInformation))
                return true;
            else
                return false;
        }

        internal async Task<bool> UpdateOrigin(Manifestoutgoing selectedItem)
        {
            var result = await client.PutAsync<Manifestoutgoing>("UpdateOrigin", selectedItem.Id, selectedItem);
            if (result != default(Manifestoutgoing))
                return true;
            else
                return false;
        }

        internal async Task< bool> UpdateDestination(Manifestoutgoing selectedItem)
        {
            var result = await client.PutAsync<Manifestoutgoing>("UpdateDestination", selectedItem.Id, selectedItem);
            if (result != default(Manifestoutgoing))
                return true;
            else
                return false;
        }

        internal async Task<List<Manifestoutgoing>> ManifestsByPenjualanId(int id)
        {
            var res= await client.GetAsync<List<Manifestoutgoing>>("ManifestsByPenjualanId", id);
            return res;
        }

        internal  async  Task<Titipankapal> GetTitipanKapal(int manifestId)
        {
            return await client.GetAsync<Titipankapal>("GetTitipanKapal",manifestId);
        }

        internal Task<List<PackingListPrintModel>> GetPackingList(int manifestId)
        {
            return client.GetAsync<List<PackingListPrintModel>>("GetPackingList", manifestId);

        }
    }
}
