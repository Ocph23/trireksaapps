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
    public class PortCollection: BaseCollection
    {

        public event RefreshComplete RefreshCompleted;
        public ObservableCollection<ModelsShared.Models.Port> Source { get; set; }
        public CollectionView SourceView { get; set; }
        private readonly Client client = new Client("Ports");
        private SignalRClient signalRClient;

        public ModelsShared.Models.Port SelectedItem { get; set; }
        public CollectionView SourceView1 { get; private set; }

        public PortCollection()
        {
            Source = new ObservableCollection<ModelsShared.Models.Port>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            CompleteTask();
        }

        public void Refresh()
        {
            CompleteTask();
        }


        public async void CompleteTask()
        {

            var result = await client.GetAsync<List<Port>>("");
            if(result!=default(List<Port>))
            {
                Source.Clear();
                foreach (var item in result)
                {
                    Source.Add(item);
               
                }
                SourceView.Refresh();
                RefreshCompleted?.Invoke();
            }

            signalRClient = ResourcesBase.GetSignalClient();
            signalRClient.OnAddPort += SignalRClient_OnAddPort;

        }

        private async void SignalRClient_OnAddPort(object sender)
        {
            await Task.Delay(3000);
            var result = JsonConvert.DeserializeObject<Port>(sender.ToString());
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

        internal async Task<Port> Add(Port port)
        {
            var result = await client.PostAsync<Port>("", port);
            if(result!=default(Port))
            {
                Source.Add(result);
                SourceView.Refresh();
            }
            return result;
         
        }

        internal async Task<bool> Update(int id, Port port)
        {
            var data = Source.Where(O => O.Id == id).FirstOrDefault();
            if (data != null)
            {
                var saved = await client.PutAsync<bool>("", id, port);
                if (saved)
                {
                    data.CityID = port.CityID;
                    data.CityName = port.CityName;
                    data.Code = port.Code;
                    data.Name = port.Name;
                    data.PortType = port.PortType;
                }
                return saved;
            }
            else
                return false;
         
        }

        internal async Task<Port> Delete(int id)
        {
            var data = Source.Where(O => O.Id == id).FirstOrDefault();
            if (data != null)
            {
                var result = await client.Delete<Port>("", id);
                if (result!=null)
                {
                    Source.Remove(data);
                    SourceView.Refresh();
                }
                return result;
            }

            return default;
               
        }
    }
}
