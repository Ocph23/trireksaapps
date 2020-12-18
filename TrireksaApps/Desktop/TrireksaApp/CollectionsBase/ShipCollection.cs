using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Data;
using ModelsShared.Models;
using TrireksaApp.Common;

namespace TrireksaApp.CollectionsBase
{
    public class ShipCollection 
    {
        private Client client = new Client("Ships");
        public Ship SelectedItem { get; set; }

        public ObservableCollection<Ship> Source { get; set; }

        public CollectionView SourceView { get; set; }

        public ShipCollection()
        {
            Source = new ObservableCollection<Ship>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            CompleteTask();
        }

        public async void CompleteTask()
        {
            var result = await client.GetAsync<List<Ship>>("Get");
            if(result!=default(List<Ship>))
            {
                foreach (var item in result)
                {
                    Source.Add(item);
                    SourceView.Refresh();
                }
            }
          
        }

        public Ship GetItemById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Ship> Add(Ship item)
        {
            var result = await client.PostAsync<Ship>("Post", item);
            if(result!=default(Ship))
            {
                Source.Add(result);
                SourceView.Refresh();
            }
            return result;
        }
    }
}
