using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrireksaMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class STTStatusPage : ContentPage
    {
        private BarcodeScannerViewModel vmBarcode;
        private STTStatusViewModel vm;

        public STTStatusPage()
        {
            InitializeComponent();
            vm = new STTStatusViewModel();
            BindingContext = vm;
        }

        private void VmBarcode_OnResultScanHandler(object obj)
        {
            stt.Text = obj.ToString();
            vm.SearchCommand.Execute(obj.ToString());
            Console.WriteLine(obj.ToString());
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            vmBarcode = new BarcodeScannerViewModel();
            vmBarcode.OnResultScanHandler += VmBarcode_OnResultScanHandler;
            var formBarcode = new BarcodeScanner() { BindingContext = vmBarcode };
            await Shell.Current.Navigation.PushModalAsync(formBarcode);
        }

        private void stt_TextChanged(object sender, TextChangedEventArgs e)
        {
            vm.STT = e.NewTextValue;
            vm.IsFound = false;

        }
    }


    public class STTStatusViewModel : BaseViewModel
    {

        public STTStatusViewModel()
        {
            SearchCommand = new Command(SearchAction, SearchValidate);
            UpdateStatusCommand = new Command(UpdateStatusAction, UpdateStatusValidate);
            this.PropertyChanged +=
              (_, __) =>
              {
                  SearchCommand.ChangeCanExecute();
                  UpdateStatusCommand.ChangeCanExecute();
              };

           // Model.DeliveryStatus.PropertyChanged += (_, __) => UpdateStatusCommand.ChangeCanExecute();
        }

        private bool UpdateStatusValidate(object arg)
        {
            if (IsBusy)
                return false;
           if(Model!=null)
            {
                if (!string.IsNullOrEmpty(Model.DeliveryStatus.ReciveName) && Model.DeliveryStatus.ReciveDate!=null)
                    return true;
            }

            return false;
        }

        private async void UpdateStatusAction(object obj)
        {
            try
            {
                IsBusy = true;
                var result = await PenjualanStore.UpdateDeliveryStatusById(Model.DeliveryStatus.Id,Model.DeliveryStatus);
                if (result != null)
                {
                   await MessageHelper.InfoAsync("Update Status Berhasil !");
                }
                else
                {
                    throw new SystemException("Status Tidak Berhasil Di Simpan !");
                }
            }
            catch (Exception ex)
            {
               await MessageHelper.ErrorAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool SearchValidate(object arg)
        {

            if (IsBusy || string.IsNullOrEmpty(STT))
                return false;
            return true;
        }

        private async void SearchAction(object obj)
        {
            try
            {
                IsBusy = true;
                var result = await PenjualanStore.GetBySTT(STT);
                if (result != null)
                {
                    IsFound = true;
                    Model = result;
                    
                }
                else
                {
                    throw new SystemException("STT tidak Ditemukan !");
                }
            }
            catch (Exception ex)
            {
                await  MessageHelper.ErrorAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
            
        }

        private string stt;
        public string STT
        {
            get { return stt; }
            set {SetProperty(ref stt , value);
            }
        }

        private Penjualan penjualan;

        public Penjualan Model
        {
            get { return penjualan; }
            set { SetProperty(ref penjualan , value); }
        }


        private bool isFound;

        public bool IsFound
        {
            get { return isFound; }
            set { SetProperty(ref isFound , value); }
        }

        public Command SearchCommand { get; }
        public Command UpdateStatusCommand { get; }
    }
}