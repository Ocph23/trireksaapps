using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows.Controls;
using ModelsShared.Models;
using ModelsShared.ReportModels;
using TrireksaApp.CollectionsBase;
using TrireksaApp.Pages;
using TrireksaApp.Reports.Models;

namespace TrireksaApp.Contents.Main
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : UserControl
    {
        public AdminBoard Board { get; }
        public DashboardModel Dashboard { get; private set; }

        public Admin()
        {
            InitializeComponent();
            this.Board = new CollectionsBase.AdminBoard();
            this.Loaded += Admin_Loaded;
            
        }

        private void Admin_Loaded(object sender, RoutedEventArgs e)
        {
            Penjualan_Loaded();
        }

        private async void Penjualan_Loaded()
        {
            await Task.Delay(100);
            busy.IsActive = true;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("id-ID");
            penjualanIni.Title.Text = "Bulan Ini";
            penjualanLalu.Title.Text = "Bulan Lalu";
            penjualanLalunya.Title.Text = "2 Bulan Lalu";
            spbbelumditagih.Title.Text = "SPB Belum Ditagih";
            spbNotStatus.Title.Text = "Status Penerima Tidak Ada";

            invoiceNotDelivery.Title.Text = "Invoice Belum Dikirim";
            invoiceNotPaid.Title.Text = "Invoice Belum Dibayar";
            invoiceNotRecive.Title.Text = "Invoice Belum Diterima";
            invoiceJatuhTempo.Title.Text = "Invoice Jatuh Tempo";
            spbbelumdikirim.Title.Text = "SPB Belum Dikirim";

            OnGetDashboard(Board.Get());

            //OnCompleteInvoice(Board.GetInvoiceNotYetPaid(), invoiceNotPaid);
            //OnCompleteInvoice(Board.GetInvoiceJatuhTempo(), invoiceJatuhTempo);
            //OnCompleteInvoice(Board.GetInvoiceNotYetRecive(), invoiceNotRecive);
            //OnCompleteInvoice(Board.GetInvoiceNotYetDelivery(), invoiceNotDelivery);

           // DateTime date = DateTime.Now;

          ////  PenjualanBulan(Board.GetPenjualanBulan(date),penjualanIni);
          //  PenjualanBulan(Board.GetPenjualanBulan(date.AddMonths(-1)), penjualanLalu);
          //  PenjualanBulan(Board.GetPenjualanBulan(date.AddMonths(-2)), penjualanLalunya);
            //OnCompletePenjualanNotHaveDeliveryStatus(Board.GetPenjualanNotPaid(), spbbelumditagih);
            //OnCompletePenjualanNotHaveDeliveryStatus(Board.GetPenjualanNotStatus(), spbNotStatus);
            //OnCompletePenjualanNotHaveDeliveryStatus(Board.GetPenjualanNotYetSend(), spbbelumdikirim);
            busy.IsActive = false;
        }

        private async void OnGetDashboard(Task<DashboardModel> task)
        {
            var res = await task;
            if (res != null && res.InvoiceJatuhTempo!=null)
            {
                Dashboard = res;
                penjualanIni.ContentItem.Text = string.Format("Rp. {0:N}", res.PenjualanBulanIni);
                penjualanLalu.ContentItem.Text = string.Format("Rp. {0:N}", res.PenjualanBulanLalu);
                penjualanLalunya.ContentItem.Text = string.Format("Rp. {0:N}", res.PenjualanDuaBulanLalu);
                invoiceJatuhTempo.ContentItem.Text = string.Format("{0} Inv", res.InvoiceJatuhTempo.Count());
                invoiceNotPaid.ContentItem.Text = string.Format("{0} Inv", res.InvoiceNotPaid.Count());
                invoiceNotRecive.ContentItem.Text = string.Format("{0} Inv", res.InvoiceNotYetRecive.Count());
                invoiceNotDelivery.ContentItem.Text = string.Format("{0} Inv", res.InvoiceNotYetDelivery.Count());
                
                spbbelumdikirim.ContentItem.Text = string.Format("{0} SPB", res.PenjualanNotYetSend.Count());
                spbbelumditagih.ContentItem.Text = string.Format("{0} SPB", res.PenjualanNotPaid.Count());
                spbNotStatus.ContentItem.Text = string.Format("{0} SPB", res.PenjualanNotHaveStatus.Count());
            }
        }

        //private async void OnCompleteInvoice(Task<List<ModelsShared.Models.Invoice>> task, MainBoxItem obj)
        //{
        //    var res = await task;
        //    if(res!=null)
        //         obj.ContentItem.Text = string.Format("{0} Inv", res.Count);
        //}

        //private async void OnCompletePenjualanNotHaveDeliveryStatus(Task<List<PenjualanReportModel>> task, MainBoxItem obj)
        //{
        //    var res = await task;
        //    if (res != null)
        //        obj.ContentItem.Text = string.Format("{0} SPB", res.Count);
        //}
        

        //private async void PenjualanBulan(Task<double> task, MainBoxItem obj)
        //{
        //    double res = await task;
        //    if (res >= 0)
        //        obj.ContentItem.Text = string.Format("Rp. {0:N}", res);
        //}

        private async void PenjualanPreview(object sender, MouseButtonEventArgs e)
        {
            var mainbox = (MainBoxItem)sender;
            List<PenjualanReportModel> list;
            switch (mainbox.Name)
            {
             
                case "spbbelumdikirim":
                    list = Dashboard.PenjualanNotYetSend.ToList();// await Board.GetPenjualanNotYetSend();
                    CallReportPenjualan("Penjualan Belum Dikirim", list);
                    break;
                case "spbbelumditagih":
                    list = Dashboard.PenjualanNotPaid.ToList();// await Board.GetPenjualanNotPaid();
                    CallReportPenjualan("Penjualan Belum Ditagih", list);
                    break;
                case "spbNotStatus":
                    list = Dashboard.PenjualanNotHaveStatus.ToList();// await Board.GetPenjualanNotStatus();
                    CallReportPenjualan("Penjualan Belum Ada Status", list);
                    break;

                default:
                    break;
            }
          
        }

        private void CallReportPenjualan(string title, List<PenjualanReportModel> list)
        {
            var content = new Reports.Contents.ReportContent(new Microsoft.Reporting.WinForms.ReportDataSource { Value = list },
                "TrireksaApp.Reports.Layouts.AdminPenjualanLayout.rdlc",new Microsoft.Reporting.WinForms.ReportParameter[] { new Microsoft.Reporting.WinForms.ReportParameter("Title",new string[] { title}) });
            var dlg = new ModernWindow
            {
                Content = content,
                Title = "Nota",
                Style = (Style)App.Current.Resources["BlankWindow"],
                ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                WindowState = WindowState.Maximized,
            };

            dlg.ShowDialog();
        }

        private async void InvoicePreview(object sender, MouseButtonEventArgs e)
        {
            await Task.Delay(100);
            var mainbox = (MainBoxItem)sender;
            List<ModelsShared.Models.Invoice> list;
            switch (mainbox.Name)
            {

                case "invoiceNotDelivery":
                    list = Dashboard.InvoiceNotYetDelivery.ToList();// await Board.GetInvoiceNotYetDelivery();
                    CallReportInvoice("Invoice Belum Dikirim", list);
                    break;
                case "invoiceNotRecive":
                    list = Dashboard.InvoiceNotYetRecive.ToList();// await Board.GetInvoiceNotYetRecive();
                    CallReportInvoice("Invoice Belum Diterima", list);
                    break;
                case "invoiceNotPaid":
                    list = Dashboard.InvoiceNotPaid.ToList();// await Board.GetInvoiceNotYetPaid();
                    CallReportInvoice("Invoice Belum Dibayar", list);
                    break;
                case "invoiceJatuhTempo":
                    list = Dashboard.InvoiceJatuhTempo.ToList();// await Board.GetInvoiceJatuhTempo();
                    CallReportInvoice("Invoice Jatuh Tempo", list);
                    break;

                default:
                    break;
            }
        }

        private void CallReportInvoice(string title, List<ModelsShared.Models.Invoice> data)
        {
           
            var list = new List<InvoiceReport>();
            foreach (var item in data)
            {
                list.Add(new InvoiceReport(item));
            }



            var content = new Reports.Contents.ReportContent(new Microsoft.Reporting.WinForms.ReportDataSource { Value = list },
                "TrireksaApp.Reports.Layouts.InvoiceReportLayout.rdlc", new Microsoft.Reporting.WinForms.ReportParameter[] { new Microsoft.Reporting.WinForms.ReportParameter("Title", new string[] { title }) });
            var dlg = new ModernWindow
            {
                Content = content,
                Title = "Nota",
                Style = (Style)App.Current.Resources["BlankWindow"],
                ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                WindowState = WindowState.Maximized,
            };

            dlg.ShowDialog();
        }
    }
}
