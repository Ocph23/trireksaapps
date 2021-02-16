using ModelsShared.Models;
using ShareModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TrireksaMobile.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TrireksaMobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private double bulanIni;
        private double bulanLalu;
        private double duaBulan;
        private double jatuhTempo;
        private double invoiceNotDelivery;
        private double invoiceNotPaid;
        private double invoiceNotRecive;
        private double sttNotPaid;
        private double sttNotSend;
        private double sttNotStatus;

        public AboutViewModel()
        {
            Title = "Home";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            DetailCommand = new Command(PenjualanAction);    
            Load();
        }

        private async void PenjualanAction(object obj)
        {
            if(!string.IsNullOrEmpty(obj.ToString()))
            {
                switch (obj.ToString())
                {
                    case "1":
                        ShowPenjualan(await DashboardStore.GetPenjualanNotYetSend(), "STT Belum Dikirim");
                        break;
                    case "2":
                        ShowPenjualan(await DashboardStore.GetPenjualanNotStatus(), "STT Belum Ada Status");
                        break;
                    case "3":
                        ShowPenjualan(await DashboardStore.GetPenjualanNotPaid(), "STT Belum Ditagih");
                        break;

                    case "4":
                        ShowInvoice(await DashboardStore.GetInvoiceNotYetDelivery(), "Invoice Belum Dikirim");
                        break;
                    case "5":
                        ShowInvoice(await DashboardStore.GetInvoiceNotYetRecive(), "Invoice Belum Diterima");
                        break;
                    case "6":
                        ShowInvoice(await DashboardStore.GetInvoiceNotYetPaid(), "Invoice Belum Dibayar");
                        break;
                    case "7":
                        ShowInvoice(await DashboardStore.GetInvoiceJatuhTempo(), "Invoice Jatuh Tempo");
                        break;

                    default:
                        break;
                }
            }
        }

        private void ShowInvoice(List<Invoice> data, string title)
        {
            var vm = new InvoiceDetailViewModel(data, title);
            var page = new InvoiceDetailView() { BindingContext = vm };
            Shell.Current.Navigation.PushAsync(page);
        }

        private void ShowPenjualan(List<PenjualanReportModel> data, string title)
        {
            var vm = new PenjualanDetailViewModel(data, title);
            var page = new PenjualanDetailView() { BindingContext = vm };
            Shell.Current.Navigation.PushAsync(page);
        }

        private  void Load()
        {
            IsBusy = true;
            var model = DashboardStore.Get().ContinueWith(async (x) => {
                var result = await x;
                if (result != null)
                {
                    BulanIni = result.PenjualanBulanIni;
                    BulanLalu = result.PenjualanBulanLalu;
                    DuaBulanLalu = result.PenjualanDuaBulanLalu;
                    JatuhTempo = result.InvoiceJatuhTempo;
                    InvoiceNotDelivery = result.InvoiceNotYetDelivery;
                    InvoiceNotPaid = result.InvoiceNotPaid;
                    InvoiceNotRecive = result.InvoiceNotYetRecive;
                    STTNotPaid = result.PenjualanNotPaid ;
                    STTNotSend = result.PenjualanNotYetSend ;
                    STTNotStatus = result.PenjualanNotHaveStatus;
                    DataSource = result;
                    IsBusy = false;
                }
            });
        }

        public ICommand OpenWebCommand { get; }
        public Command DetailCommand { get; }
        public double BulanIni { get => bulanIni; set => SetProperty(ref bulanIni, value); }
        public double BulanLalu { get => bulanLalu; set => SetProperty(ref bulanLalu, value); }
        public double DuaBulanLalu { get => duaBulan; set => SetProperty(ref duaBulan, value); }
        public double STTNotPaid{ get => sttNotPaid; set => SetProperty(ref sttNotPaid, value); }
        public double STTNotStatus { get => sttNotStatus; set => SetProperty(ref sttNotStatus, value); }
        public DashboardModel DataSource { get; private set; }
        public double STTNotSend { get => sttNotSend; set => SetProperty(ref sttNotSend, value); }

        public double JatuhTempo { get => jatuhTempo; set => SetProperty(ref jatuhTempo, value); }
        public double InvoiceNotDelivery { get => invoiceNotDelivery; set => SetProperty(ref invoiceNotDelivery, value); }
        public double InvoiceNotPaid{ get => invoiceNotPaid; set => SetProperty(ref invoiceNotPaid, value); }
        public double InvoiceNotRecive { get => invoiceNotRecive; set => SetProperty(ref invoiceNotRecive, value); }
    }
}