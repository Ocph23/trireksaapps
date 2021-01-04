using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using ModelsShared.Models;
using TrireksaApp.Common;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;

namespace TrireksaApp.Contents.Penjualan
{
    public  class PenjualanListVM:ViewModelBase
    {
        private ModelsShared.Models.Penjualan _selected;
        private string _search;
        private readonly Dictionary<int, bool> IsDeliverys;

        public PenjualanListVM()
        {
            this.IsDeliverys = new Dictionary<int, bool>();
            this.SMSCommand = new SMSCommandHandler();
            TypeOfWeights = Common.ResourcesBase.GetEnumCollection<TypeOfWeight>();
            this.STTOnManifests = new ObservableCollection<Manifestoutgoing>();
            this.ManifestView = (CollectionView)CollectionViewSource.GetDefaultView(this.STTOnManifests);
            MainVM.PenjualanCollection.SourceView.SortDescriptions.Add(new System.ComponentModel.SortDescription("STT", System.ComponentModel.ListSortDirection.Descending));
            MainVM.PenjualanCollection.SourceView.Filter = FilterItem;
            PrintNota= new Common.CommandHandler { CanExecuteAction = PrintNotaValidation, ExecuteAction =  PrintNotaAction };
            AllDetails = new Common.CommandHandler { CanExecuteAction = x => AllDetailValidation(), ExecuteAction = x => AllDetailAction() };
            PrintPreviewNota = new Common.CommandHandler { CanExecuteAction = PrintNotaValidation, ExecuteAction = PrintPreviewNotaAction };
            this.SMSCommand.SendToShiper = new CommandHandler { CanExecuteAction = SendToShiperValidate, ExecuteAction = x => SentMessage() };
            this.SMSCommand.SendToReciver = new CommandHandler { CanExecuteAction=SendToReciverValidate, ExecuteAction = x => SentMessageReciver()  };
            UpdateDeliveryStatusCommand = new CommandHandler { CanExecuteAction=x=>UpdateDeliveryStatusValidation(), ExecuteAction = UpdateDeliveryStatusAction };
            MainVM.PenjualanCollection.RefreshCompleted += PenjualanCollection_RefreshCompleted;
            RefreshAction(null);
        }

        private void PrintPreviewNotaAction(object obj)
        {
            Helper.PrintPreviewNotaAction(SelectedItem);
        }

        private void PenjualanCollection_RefreshCompleted()
        {
            if (ProgressIsActive)
                ProgressIsActive = false;
        }

        protected override void RefreshAction(object obj)
        {
            ProgressIsActive = true;
            MainVM.PenjualanCollection.Refresh();
        }

        private bool SendToReciverValidate(object obj)
        {
            if (SelectedItem != null)
                return true;
            else
                return false;
        }

        private bool SendToShiperValidate(object obj)
        {
            if (SelectedItem != null)
                return true;
            else
                return false;
        }

        private bool UpdateDeliveryStatusValidation()
        {
            if (SelectedItem != null)
            {

                var isDelivery = false;
                var status = IsDeliverys.Where(O => O.Key == SelectedItem.Id).FirstOrDefault();
                var uri = string.Format("{0}?Id={1}", Common.Helper.GetApiUrl<ModelsShared.Models.Penjualan>("IsSended"), SelectedItem.Id);
                if (status.Key != SelectedItem.Id)
                {
                    MainVM.PenjualanCollection.IsSended().ContinueWith(async O =>
                    {
                        bool result = await O;
                        IsDeliverys.Add(SelectedItem.Id, result);
                        isDelivery = result;

                    });
                }
                else
                {
                    isDelivery = status.Value;
                }
                return !isDelivery;
            }
            else
                return false;
        }

        private async void UpdateDeliveryStatusAction(object obj)
        {
            var success = await MainVM.PenjualanCollection.UpdateDeliveryStatus(SelectedItem.DeliveryStatus);
            if (success)
            {
                ModernDialog.ShowMessage("Status Tersimpan", "Success", MessageBoxButton.OK);
            }
            else
            {
                ModernDialog.ShowMessage("Status Tidak Tersimpan", "Error", MessageBoxButton.OK);
            }
        }

        private void SentMessageReciver()
        {
            //var message = new OutMessage
            //{
            //    Destination = SelectedItem.Reciver.Handphone,
            //    MessageText = string.Format("STT:{0:D5}, Reciver={1}, Cly={2}, Weight={3}", SelectedItem.STT,
            //      SelectedItem.Shiper.Name, SelectedItem.Details.Count, SelectedItem.Details.Sum(O => O.Weight))
            //};

            //Common.ResourcesBase.GetMainWindowViewModel().SMS.SendMessage(message);
        }

        private void SentMessage()
        {
            //var message = new OutMessage
            //{
            //    Destination = SelectedItem.Shiper.Handphone,
            //    MessageText = string.Format("STT:{0:D5}, Reciver={1}, Cly={2}, Weight={3}", SelectedItem.STT,
            //      SelectedItem.Reciver.Name, SelectedItem.Details.Count, SelectedItem.Details.Sum(O => O.Weight))
            //};

            //Common.ResourcesBase.GetMainWindowViewModel().SMS.SendMessage(message);
        }
   

        private void PrintNotaAction(object obj)
        {
            Helper.PrintNotaAction(SelectedItem);
        }

        private bool PrintNotaValidation(object obj)
        {
            if (SelectedItem != null)
                return true;
            else
                return false;
        }

        private void AllDetailAction()
        {
            var man = new Penjualan.PenjualanInfoVM(SelectedItem);

            var content = new PenjualanInfo
            {
                DataContext = man
            };
            var dlg = new ModernWindow
            {
                Content = content,
                Title = "Details",
                Style = (Style)App.Current.Resources["BlankWindow"],
               // ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                WindowState = WindowState.Normal, WindowStartupLocation= WindowStartupLocation.CenterScreen
            };

            dlg.ShowDialog();
        }

        private bool AllDetailValidation()
        {
            if (SelectedItem != null)
                return true;
            else
                return false;
        }

       
        public ModelsShared.Models.Penjualan SelectedItem
        {
            get { return _selected; }
            set
            {

                _selected = value;
                if (_selected != null)
                {
                    GetSendedInformation(_selected);
                    if (_selected.DeliveryStatus == null)
                    {
                        _selected.DeliveryStatus =  new Deliverystatus { PenjualanId = _selected.Id  };
                    }
                }
                OnPropertyChanged("SelectedItem");
            }
        }

        private async void GetSendedInformation(ModelsShared.Models.Penjualan _selected)
        {
            var result = await MainVM.ManifestOutgoingCollection.ManifestsByPenjualanId(_selected.Id);
            STTOnManifests.Clear();
            if (result != null)
            {
                foreach (var item in result)
                {
                    this.STTOnManifests.Add(item);
                }
                this.ManifestView.Refresh();
            }
        }



        //Filter Property
        private bool FilterItem(object x)
        {
            if (!String.IsNullOrEmpty(this.Search))
            {
                string scr = this.Search.ToUpper();
                var obj = (ModelsShared.Models.Penjualan)x;
                return obj.STT.ToString().Contains(scr) || obj.Shiper.Name.ToUpper().Contains(scr) || obj.Reciver.Name.ToString().ToUpper().Contains(scr);
            }
            else
                return true;
        }


        public string Search
        {
            get
            {
                return _search;
            }
            set
            {
                _search = value;
                MainVM.PenjualanCollection.SourceView.Refresh();
                OnPropertyChanged("Search");
            }
        }

        public List<TypeOfWeight> TypeOfWeights { get; set; }
        public ObservableCollection<Manifestoutgoing> STTOnManifests { get; set; }
        public CollectionView ManifestView { get; private set; }
        public CommandHandler ManifestPreview { get; private set; }
        public Manifestoutgoing ManifestSelected { get; set; }
        public CommandHandler AllDetails { get; private set; }
        public CommandHandler PrintPreviewNota { get; }
        public CommandHandler PrintNota { get; private set; }
        public SMSCommandHandler SMSCommand { get; private set; }
        public CommandHandler UpdateDeliveryStatusCommand { get; set; }
    }
}
