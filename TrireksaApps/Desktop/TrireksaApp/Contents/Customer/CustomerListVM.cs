using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Windows.Controls;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Customer
{
    public class CustomerListVM : ViewModelBase
    {
        private string _search;

        public CollectionsBase.CustomerCollection Collection { get; set; }
        public CustomerListVM()
        {
            Delete = new CommandHandler { CanExecuteAction = x => DeleteValidation(), ExecuteAction = x => DeleteAction() };
            Edit = new CommandHandler { CanExecuteAction = x => EditValidation(), ExecuteAction = x => EditAction() };
            RegisterCustomer = new CommandHandler { CanExecuteAction = RegiterValidation, ExecuteAction = RegisterAction };
            Collection = MainVM.CustomerCollection;
            Collection.SourceView.Filter = FilterItem;


        }

        protected override void RefreshAction(object obj)
        {
            ProgressIsActive = true;
            MainVM.CustomerCollection.Refresh();
        }

        private bool RegiterValidation(object obj)
        {
            return true;
        }

        private async void RegisterAction(object obj)
        {
            await Collection.RegisterCustomer(Collection.SelectedItem);
        }

        //private async void RegisterAsyncHandler(HttpResponseMessage response)
        //{
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var result = await response.Content.ReadAsAsync<customer>();
        //        if (result != null)
        //        {
        //            var MainVM = Common.ResourcesBase.GetMainWindowViewModel();
        //            await MainVM.MessageCollection.Add(new Message { MessageText = string.Format("{0} Is Registered", result.Name) });
        //        }
        //    }
        //}

        private async void EditAction()
        {


            var vm = new CustomerEditVM(Collection.SelectedItem);
            var cnt = new Edit
            {
                DataContext = vm
            };
            var dlg = new ModernDialog  
            {   
                Width=800,
                Title = "Edit Customer",
                Content = cnt
            };
            dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
            dlg.ShowDialog();


            if (dlg.MessageBoxResult == System.Windows.MessageBoxResult.OK)
            {
                var newitem = new ModelsShared.Models.Customer
                {
                    Address = vm.Address,
                    ContactName = vm.ContactName,
                    CustomerType = vm.CustomerType,
                    Email = vm.Email,
                    Handphone = vm.Handphone,
                    Id = vm.Id,
                    Name = vm.Name,
                    Phone1 = vm.Phone1,
                    Phone2 = vm.Phone2,
                    CityID = vm.CityID
                };
                var isUpdate = await Collection.Update(Collection.SelectedItem.Id, newitem);
                if (isUpdate)
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
                if (isDeleted)
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
            if (!String.IsNullOrEmpty(this.Search))
            {
                string scr = this.Search.ToUpper();
                var obj = (ModelsShared.Models.Customer)x;
                return obj.Name.ToUpper().Contains(scr) || obj.CustomerType.ToString().ToUpper().Contains(scr)
                    || obj.ContactName.ToUpper().Contains(scr);
            }
            else
                return true;
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

        public CommandHandler RegisterCustomer { get; private set; }
    }
}