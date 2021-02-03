using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Port
{
    public class PortListVM : ViewModelBase
    {
        private string _search;

        private PortType portfilter;

        public PortType PortFilter
        {
            get { return portfilter; }
            set { portfilter = value; Collection.SourceView.Refresh(); }
        }


        public CollectionsBase.PortCollection Collection { get; set; }
        public PortListVM()
        {
            Delete = new CommandHandler { CanExecuteAction = x => DeleteValidation(), ExecuteAction = x => DeleteAction() };
            Edit = new CommandHandler { CanExecuteAction = x => EditValidation(), ExecuteAction = x => EditAction() };
            this.Collection = MainVM.PortCollection;
            Collection.SourceView.Filter = FilterItem;
            PortFilter = PortType.Sea;
            MainVM.PortCollection.RefreshCompleted += PortCollection_RefreshCompleted;

        }

        private void PortCollection_RefreshCompleted()
        {
            Collection.SourceView.Refresh();
        }

        protected  override void RefreshAction(object obj)
        {
            ProgressIsActive = true;
            Collection.SourceView.Filter = null;
            Collection.SourceView.Filter = FilterItem;
            MainVM.PortCollection.Refresh();
        }


        private async void EditAction()
        {
            var vm = new PortEditVM(Collection.SelectedItem);
            var cnt = new Edit
            {
                DataContext = vm
            };
            var dlg = new ModernDialog
            {
                Title = "Edit",
                Content = cnt
            };
            dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
            dlg.ShowDialog();

            if (dlg.DialogResult.Value)
            {
                ModelsShared.Models.Port port = new ModelsShared.Models.Port
                {
                    CityID = vm.CityID,
                    Code = vm.Code,
                    PortType = vm.PortType,
                    Id = vm.Id,
                    Name = vm.Name,
                    CityName =MainVM.CityCollection.Source.Where(O => O.Id == vm.CityID).FirstOrDefault().CityName
                };
                var isUpdated = await Collection.Update(port.Id, port);
                if (isUpdated)
                {
                    Collection.SourceView.Refresh();
                    ModernDialog.ShowMessage("Data Is Updated !", "Message Dialog", System.Windows.MessageBoxButton.OK);
                }
            }

        }

        private async void DeleteAction()
        {
            var result = ModernDialog.ShowMessage("Are You Sure  Delete Data ?", "Message Dialog", System.Windows.MessageBoxButton.YesNo);
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                var isDeleted = await Collection.Delete(Collection.SelectedItem.Id);
                if (isDeleted!=default(ModelsShared.Models.Port))
                {
                    Collection.SourceView.Refresh();
                    ModernDialog.ShowMessage("Data Is Deleted !", "Message Dialog", System.Windows.MessageBoxButton.OK);
                }

            }

        }

        private bool EditValidation()
        {
            if (Collection.SelectedItem != null)
                return true;
            else
                return false;
        }

        private bool DeleteValidation()
        {
            if (Collection.SelectedItem != null)
                return true;
            else
                return false;
        }

        public CommandHandler Delete { get; set; }
        public CommandHandler Edit { get; set; }

        private bool FilterItem(object x)
        {
            var obj = (ModelsShared.Models.Port)x;

            if (!string.IsNullOrEmpty(this.Search))
            {
                var scr = this.Search.ToUpper();
                return (PortFilter== PortType.None ? true : obj.PortType==PortFilter) &&( (obj.Code.ToUpper().Contains(scr)
                    || obj.City.CityName.ToUpper().Contains(scr)
                    || obj.Name.ToUpper().Contains(scr)));
            }
            else
            {
                if (obj != null && PortFilter != PortType.None)
                    return obj.PortType == PortFilter;
                return true;
            }
               
        }

        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Search
        {

            get { return _search; }
            set
            {
                _search = value;
                Collection.SourceView.Refresh();
                OnPropertyChanged("Search");
            }
        }
    }
}