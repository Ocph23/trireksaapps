using ModelsShared.Models;
using ModelsShared.ReportModels;
using System;
using System.Collections.Generic;
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

        private void PenjualanAction(object obj)
        {
            if(!string.IsNullOrEmpty(obj.ToString()))
            {
                switch (obj.ToString())
                {
                    case "1":
                        ShowPenjualan(DataSTTNotSend, "STT Belum Dikirim");
                        break;
                    case "2":
                        ShowPenjualan(DataSTTNotStatus, "STT Belum Ada Status");
                        break;
                    case "3":
                        ShowPenjualan(DataSTTNotPaid, "STT Belum Ditagih");
                        break;

                    case "4":
                        ShowInvoice(DataInvoiceNotDelivery, "Invoice Belum Dikirim");
                        break;
                    case "5":
                        ShowInvoice(DataInvoiceNotRecive, "Invoice Belum Diterima");
                        break;
                    case "6":
                        ShowInvoice(DataInvoiceNotPaid, "Invoice Belum Dibayar");
                        break;
                    case "7":
                        ShowInvoice(InvoiceJatuhTempo, "Invoice Jatuh Tempo");
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
            var now = DateTime.Now;
            DashboardStore.GetPenjualanBulan(now).ContinueWith(async (x)=> {
               BulanIni= await x;
            });
            DashboardStore.GetPenjualanBulan(now.AddMonths(1)).ContinueWith(async (x) => {
                BulanLalu = await x;
            });
            DashboardStore.GetPenjualanBulan(now.AddMonths(2)).ContinueWith(async(x) => {
                DuaBulanLalu = await x;
            });

            DashboardStore.GetInvoiceJatuhTempo().ContinueWith(async (x) => {
                InvoiceJatuhTempo = await x;
                JatuhTempo = InvoiceJatuhTempo == null ? 0 : InvoiceJatuhTempo.Count;
            });

            DashboardStore.GetInvoiceNotYetDelivery().ContinueWith(async (x) => {
                DataInvoiceNotDelivery= await x;
                InvoiceNotDelivery = DataInvoiceNotDelivery == null ? 0 : DataInvoiceNotDelivery.Count;
            });

            DashboardStore.GetInvoiceNotYetPaid().ContinueWith(async (x) => {
                DataInvoiceNotPaid= await x;
                InvoiceNotPaid= DataInvoiceNotPaid == null ? 0 : DataInvoiceNotPaid.Count;
            });

            DashboardStore.GetInvoiceNotYetRecive().ContinueWith(async (x) => {
                DataInvoiceNotRecive= await x;
                InvoiceNotRecive = DataInvoiceNotRecive == null ? 0 : DataInvoiceNotRecive.Count;
            });

            DashboardStore.GetPenjualanNotPaid().ContinueWith(async (x) => {
                DataSTTNotPaid = await x;
                STTNotPaid= DataSTTNotPaid == null ? 0 : DataSTTNotPaid.Count;
            });
            DashboardStore.GetPenjualanNotStatus().ContinueWith(async (x) => {
                DataSTTNotStatus= await x;
                STTNotStatus= DataSTTNotStatus == null ? 0 : DataSTTNotStatus.Count;

            });

            DashboardStore.GetPenjualanNotYetSend().ContinueWith(async (x) => {
                DataSTTNotSend= await x;
                STTNotSend= DataSTTNotSend== null ? 0 : DataSTTNotSend.Count;
            });
        }

        public ICommand OpenWebCommand { get; }
        public Command DetailCommand { get; }
        public double BulanIni { get => bulanIni; set => SetProperty(ref bulanIni, value); }
        public double BulanLalu { get => bulanLalu; set => SetProperty(ref bulanLalu, value); }
        public double DuaBulanLalu { get => duaBulan; set => SetProperty(ref duaBulan, value); }
        public double STTNotPaid{ get => sttNotPaid; set => SetProperty(ref sttNotPaid, value); }
        public double STTNotStatus { get => sttNotStatus; set => SetProperty(ref sttNotStatus, value); }
        public double STTNotSend { get => sttNotSend; set => SetProperty(ref sttNotSend, value); }

        public double JatuhTempo { get => jatuhTempo; set => SetProperty(ref jatuhTempo, value); }
        public double InvoiceNotDelivery { get => invoiceNotDelivery; set => SetProperty(ref invoiceNotDelivery, value); }
        public double InvoiceNotPaid{ get => invoiceNotPaid; set => SetProperty(ref invoiceNotPaid, value); }
        public double InvoiceNotRecive { get => invoiceNotRecive; set => SetProperty(ref invoiceNotRecive, value); }
       



        public List<Invoice> InvoiceJatuhTempo { get; private set; }
        public List<Invoice> DataInvoiceNotDelivery { get; private set; }
        public List<Invoice> DataInvoiceNotPaid { get; private set; }
        public List<Invoice> DataInvoiceNotRecive { get; private set; }
        public List<PenjualanReportModel> DataSTTNotPaid { get; private set; }
        public List<PenjualanReportModel> DataSTTNotStatus { get; private set; }
        public List<PenjualanReportModel> DataSTTNotSend { get; private set; }
    }
}