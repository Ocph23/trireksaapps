using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsShared.Models;
using TrireksaApp.Common;
using System.ComponentModel;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Ink;
using TrireksaApp.Templates;

namespace TrireksaApp.Contents.Ship
{
    public class ShipCreateVM : ModelsShared.Models.Ship, IDataErrorInfo
    {
        public ShipCreateVM()
        {
           
            Save = new CommandHandler { CanExecuteAction = x => SaveValidation(), ExecuteAction = x => SaveAction() };
            Cancel = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => CancelAction() };
            AddRoute = new CommandHandler { CanExecuteAction = x => true, ExecuteAction =  AddRouteActions };
            this.Ports = new ObservableCollection<Portview>();
            this.MainVM = Common.ResourcesBase.GetMainWindowViewModel();
            PortsSource = MainVM.PortCollection.Source.Where(O => O.PortType == PortType.Sea).ToList();


        }

        private void AddRouteActions(object obj)
        {
           

            var cnt = new Ship.BrowseRoute();
            var dlg = new ModernDialog()
            {

                Title = "Ports",
                ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                Content = cnt
            };

            dlg.ShowDialog();

            if (cnt.SelectedItem != null && obj!=null) 
            {
                var stackpanel = (WrapPanel)obj;


                stackpanel.Children.Add(new RouteView(cnt.SelectedItem.Name));

                this.Ports.Add(new Portview { Number = 1, Name = cnt.SelectedItem.Name });

            }
        }

        private void CancelAction()
        {
            Name = string.Empty;
            Description = string.Empty;
            Route = string.Empty;
            Ports.Clear();
        }

        public CommandHandler Save { get; set; }
        public CommandHandler Cancel { get; set; }
        public CommandHandler AddRoute { get; set; }
        public CommandHandler DeleteRoute { get; set; }


        private async void SaveAction()
        {
            var list = new List<string>();
            foreach (var i in Ports)
            {
                list.Add(i.Name);
            }
            this.RouteView = list;
            var item = new ModelsShared.Models.Ship { Description = this.Description, Name = this.Name, Id = this.Id, Route = this.Route };
            var result = await MainVM.ShipCollection.Add(item);
            if (result == null)
            {
                ResourcesBase.ShowMessageError("Data Tidak Tersimpan");
            }
            else
                Cancel.Execute(null);
        }

       

        private  bool SaveValidation()
        {
            if (string.IsNullOrEmpty(this.Name) || string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(Route))
                return false;
           
            return true;
        }

        public ObservableCollection<Portview> Ports { get; private set; }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public List<ModelsShared.Models.Port> PortsSource { get; set; }

        public string this[string columnName]
        {
            get
            {
                if(columnName=="Name")
                {
                    return string.IsNullOrEmpty(this.Name) ? "Ship Name Is Required" : null;
                }
                if (columnName == "Description")
                {
                    return string.IsNullOrEmpty(this.Description) ? "Description Is Required" : null;
                }
                return null;
            }
        }


        private object _selected;

        public object SelectedItem
        {
            get { return _selected; }
            set
            {
                SetProperty(ref _selected, value);
            }
        }

        public MainWindowVM MainVM { get; private set; }
    }



    public class Portview : ModelsShared.Models.Port
    {
        public int Number { get; set; }
        public Thickness Margin { get; set; }
    }
}