using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrireksaApp.Common;
using ModelsShared.Models;
using System.Windows.Data;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Reporting.WinForms;

namespace TrireksaApp.Contents.Invoice
{
    public class InvoiceCreateVM: ModelsShared.Models.Invoice
    {
        public InvoiceCreateVM()
        {
            this.CreateDate = DateTime.Now;
            this.MainVM = Common.ResourcesBase.GetMainWindowViewModel();
            this.Invoicedetail = new List<Invoicedetail>();
            this.SourceView = (CollectionView)CollectionViewSource.GetDefaultView(this.Invoicedetail);
            this.DeadLine = DateTime.Now.AddMonths(1);
            Save = new CommandHandler { CanExecuteAction = x => SaveValidation(), ExecuteAction = x => SaveAction() };
            PreviewManifest= new CommandHandler { CanExecuteAction = x => PreviewManifestValidation(), ExecuteAction = x => PreviewManifestAction() };
            SaveAndPrint = new CommandHandler { CanExecuteAction = x => SaveValidation(), ExecuteAction = x => SaveAndPrintAction() };
            MainVM.CustomerCollection.SourceView.Filter = ShiperFilter;
        }

        internal async Task SetInvoice(int data)
        {
            if (data > 0)
            {
                var invoice = await MainVM.InvoiceCollections.GetItemById(data);
                if (invoice != null)
                {

                    this.CreateDate = invoice.CreateDate;
                    this.Customer = invoice.Customer;
                    this.CustomerId = invoice.CustomerId;
                    this.CustomerSelectedItem = invoice.Customer;
                    this.DeadLine = invoice.DeadLine;
                    this.DeliveryDate = invoice.DeliveryDate;
                    this.Id = invoice.Id;
                    this.InvoicePayType = invoice.InvoicePayType;
                    this.InvoiceStatus = invoice.InvoiceStatus;
                    this.IsDelivery = invoice.IsDelivery;
                    this.Number = invoice.Number;
                    this.PaidDate = invoice.PaidDate;
                    this.ReciveDate = invoice.ReciveDate;
                    this.ReciverBy = invoice.ReciverBy;
                    this.Tax = invoice.Tax;
                    await Task.Delay(3000);
                    foreach (var item in invoice.Invoicedetail)
                    {
                        item.IsSelected = true;
                        this.Invoicedetail.Add(item);
                    }

                    this.SourceView.Refresh();
                }
            }
            else
            {
                this.Invoicedetail.Clear();
                this.CreateDate = DateTime.Now;
                this.Customer = null;
                this.CustomerId = 0;
                this.CustomerSelectedItem = null;
                this.DeadLine = CreateDate.AddMonths(1);
                this.DeliveryDate = null;
                this.Id = 0;
                this.InvoicePayType = InvoicePayType.None;
                this.InvoiceStatus = InvoiceStatus.None;
                this.IsDelivery = false;
                this.Number = 0;
                this.PaidDate = null;
                this.ReciveDate = null;
                this.ReciverBy = string.Empty;
                this.Tax = 0;
                await Task.Delay(3000);
                this.SourceView.Refresh();
            }



            
        }

        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set {
              SetProperty(ref  searchText , value);
                MainVM.CustomerCollection.SourceView.Refresh();
            }
        }


        private bool ShiperFilter(object x)
        {
            var obj = (ModelsShared.Models.Customer)x;

            if (obj != null && !string.IsNullOrEmpty(SearchText))
            {
                string scr = SearchText.ToUpper().Trim();
                var data = obj.Name.ToUpper();
                return data.Contains(scr);
            }
            return true;
        }

        private void SaveAndPrintAction()
        {
            Save.Execute(null);

            if (this.Number > 0)
            {
                var print = new HelperPrint();
                var data = this.GetInvoiceReportModel();
                var config = HelperPrint.GetReportSetting();
                var configSource = new ReportDataSource { Value = config, Name = "Config" };
                config.FirstOrDefault().SignName = ResourcesBase.User.FullName ?? ResourcesBase.User.UserName;
                var dataSource = new ReportDataSource { Value = data.OrderBy(x => x.STT), Name = "DataSet1" };
                var datasources = new List<ReportDataSource> { dataSource, configSource };
                print.PrintDocument(datasources, "TrireksaApp.Reports.Layouts.InvoiceLayout.rdlc", null);
            }
        }

        private void PreviewManifestAction()
        {
            var data = this.GetInvoiceReportModel();
            var config = HelperPrint.GetReportSetting();
            var configSource = new ReportDataSource { Value = config, Name = "Config" };
            config.FirstOrDefault().SignName = ResourcesBase.User.FullName ?? ResourcesBase.User.UserName;
            var dataSource = new ReportDataSource { Value = data.OrderBy(x => x.STT), Name = "DataSet1" };
            var datasources = new List<ReportDataSource> { dataSource, configSource };

            var content = new Reports.Contents.ReportContent(datasources, "TrireksaApp.Reports.Layouts.InvoiceLayout.rdlc", null);
            var dlg = new FirstFloor.ModernUI.Windows.Controls.ModernWindow
            {
                Content = content,
                Title = "Invoice",
                Style = (Style)App.Current.Resources["BlankWindow"],
                ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                WindowState = WindowState.Maximized,
            };

            dlg.ShowDialog();
        }

        private List<Reports.Models.InvoiceReportModel> GetInvoiceReportModel()
        {
           var result = from item in this.Invoicedetail.Where(o => o.IsSelected)
                         select new Reports.Models.InvoiceReportModel { Id =item.Penjualan.Id,  Pcs =item.Penjualan.Pcs, Price = item.Penjualan.Price, STT =item.Penjualan.STT, ChangeDate=item.Penjualan.ChangeDate,
                             Tujuan =item.Penjualan.ToCityNavigation.CityName, DoNumber =item.Penjualan.DoNumber, Via =item.Penjualan.PortType.ToString(),  PortType=item.Penjualan.PortType,  
                          Reciver=item.Penjualan.Reciver.Name, Shiper=item.Penjualan.Shiper.Name,  PenjualanId=item.PenjualanId, Total=item.Penjualan.Total, Weight=item.Penjualan.Weight,
                           Etc=item.Penjualan.Etc, Tax=item.Penjualan.Tax, PackingCosts=item.Penjualan.PackingCosts, InvoiceId=item.InvoiceId,   Terbilang=GrandTotal.Terbilang(),
                         NumberView=this.NumberView, CustomerName=this.CustomerSelectedItem.Name, DeadLine=this.DeadLine,   CreateDate=item.Penjualan.ChangeDate};
            return result.ToList();
        }

        private bool PreviewManifestValidation()
        {
            return true;
        }

        private async void SaveAction()
        {
            if (ProgressIsActive)
                return;

            try
            {
                ProgressIsActive = true;
                var item = new ModelsShared.Models.Invoice
                {
                    Customer = CustomerSelectedItem,
                    CustomerId = this.CustomerId,
                    CreateDate = this.CreateDate,
                    CustomerName = this.CustomerName,
                    DeadLine = this.DeadLine,
                    DeliveryDate = this.DeliveryDate,
                    Invoicedetail = this.Invoicedetail.Where(O => O.IsSelected).ToList(),
                    Id = this.Id,
                    InvoicePayType = this.InvoicePayType,
                    InvoiceStatus = this.InvoiceStatus,
                    IsDelivery = this.IsDelivery,
                    Number = this.Number,
                    ReciveDate = this.ReciveDate,
                    ReciverBy = this.ReciverBy,
                    UserId = this.UserId
                };

                var saved = false;

                if(item.Id<=0)
                {
                    saved = await MainVM.InvoiceCollections.Add(item);
                }

                else
                {
                    saved = await MainVM.InvoiceCollections.Update(item);
                }

                if (saved)
                {
                    this.Id = MainVM.InvoiceCollections.SelectedItem.Id; ;
                    MainVM.InvoiceCollections.SourceView.Refresh();
                    this.Number = MainVM.InvoiceCollections.SelectedItem.Number;
                    ModernDialog.ShowMessage("Data Is Saved... !", "Information", MessageBoxButton.OK);
                }
                else
                {
                    ModernDialog.ShowMessage("Data Not Saved... !", "Error", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "Error", MessageBoxButton.OK);
            }
            finally
            {
                ProgressIsActive = false;
            }
        }

        private bool SaveValidation()
        {
            if (ProgressIsActive)
                return false;
            if (this.Invoicedetail!=null && this.Invoicedetail.Where(x=>x.IsSelected).Count()>0)
                return true;
            return false;
        }

        //private bool AddItemValidated()
        //{
        //    return true;
        //}

        private ModelsShared.Models.Customer _customer;
        private double _grand;
        private bool _isActive;

        public bool ProgressIsActive
        {
            get { return _isActive; }
            set
            {
                SetProperty(ref _isActive, value);
            }
        }

        public ModelsShared.Models.Customer CustomerSelectedItem
        {
            get
            {
                return _customer;
            }
            set
            {
                SetProperty(ref _customer, value);

                LoadPenjualan(value);
            }
        }

        private async void LoadPenjualan(ModelsShared.Models.Customer value)
        {
            if(value!=null)
            {
                ProgressIsActive = true;
                this.Invoicedetail.Clear();
                SourceView.Refresh();
                var x = await MainVM.PenjualanCollection.GetPenjualanNotPaid(value.Id);

                if (x != null && x.Count > 0)
                {
                    var r = (from item in x
                             join c in MainVM.CityCollection.Source on item.Reciver.CityID equals c.Id
                             select new ModelsShared.Models.Invoicedetail
                             {
                                 PenjualanId =item.Id,
                                Penjualan = item
                             }).ToList();

                    foreach (var item in r)
                    {
                        item.PropertyChanged += (m, y) => {
                            Item_TotalAction();
                            Save.CanExecute(null);
                        } ;
                        Invoicedetail.Add(item);


                    }
                }
                SourceView.Refresh();
                ProgressIsActive = false;
            }
        }


        private async void Item_TotalAction()
        {
            await Task.Delay(200);
            GrandTotal = this.Invoicedetail.Where(O=>O.IsSelected).Sum(O => O.Penjualan.Total);
        }
        

        public double GrandTotal
        {
            get {
                return _grand; }
            set {
                SetProperty(ref _grand, value);
            }
        }



        public MainWindowVM MainVM { get; private set; }
        public CollectionView SourceView { get; private set; }
        public CommandHandler AddItems { get; private set; }
        public CommandHandler PreviewManifest { get; private set; }
        public CommandHandler SaveAndPrint { get; private set; }
        public CommandHandler Save { get; private set; }
    }
}
