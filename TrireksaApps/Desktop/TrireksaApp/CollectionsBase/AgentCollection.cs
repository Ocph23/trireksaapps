﻿using FirstFloor.ModernUI.Presentation;
using ModelsShared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TrireksaApp.Common;

namespace TrireksaApp.CollectionsBase
{
   public  class AgentCollection
    {
        public ObservableCollection<Agent> Source { get; set; }
        public CollectionView SourceView { get; set; }
        Client client = new Client("agents");
        public Agent SelectedItem { get; set; }
        public CitiesAgentCanAccessCollection AgenCanAccess = new CitiesAgentCanAccessCollection();
        private SignalRClient signalRClient;

        public AgentCollection()
        {
            Source = new ObservableCollection<ModelsShared.Models.Agent>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            Refresh();
        }

        public Task Refresh()
        {
            Init();
            return Task.FromResult(0);
        }

        private async void Init()
        {
            Source.Clear();
            var res = await client.GetAsync<List<Agent>>("");
            if (res != null && res.Count > 0)
            {
                foreach (var item in res)
                {
                    Source.Add(item);

                }
            }
            SourceView.Refresh();
            signalRClient = ResourcesBase.GetSignalClient();
            signalRClient.OnAddAgent += SignalRClient_OnAddAgent;
        }



        private async  void SignalRClient_OnAddAgent(object sender)
        {
            await Task.Delay(2000);
            var result = JsonConvert.DeserializeObject<Agent>(sender.ToString());

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

        public Agent GetItemById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Add(Agent item)
        {
            var res = await client.PostAsync<Agent>("", item);
            if (res != null)
            {
                Source.Add(res);
                SourceView.Refresh();
                return true;
            }
            else
                return false;

        }

        internal async Task<bool> Delete(int id)
        {
            var onitem = Source.Where(O => O.Id == id).FirstOrDefault();
            
            if(onitem!=null && await client.Delete<bool>("",id))
            {
                Source.Remove(onitem);
                SourceView.Refresh();
                return true;
            }
            return false;
        }

        internal async Task< bool> Update(int id, Agent newitem)
        {
            var res = await client.PutAsync<bool>("", newitem.Id, newitem);
            SourceView.Refresh();
            if (res)
            {
                return true;
            }
            else
                return false;
           
        }
    }
}
