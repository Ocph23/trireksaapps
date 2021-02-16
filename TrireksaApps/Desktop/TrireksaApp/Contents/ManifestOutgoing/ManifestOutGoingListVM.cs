using TrireksaApp.CollectionsBase;
using ModelsShared.Models;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using TrireksaApp.Models;
using TrireksaApp.Common;
using System.Collections.Generic;
using TrireksaApp.Reports;
using System.Linq;

namespace TrireksaApp.Contents.ManifestOutgoing
{
    public class ManifestOutGoingListVM : ViewModelBase
    {
        private Manifestoutgoing _selected;
        private bool _originISync;
        private bool _destinationAsyn;
        private bool _updateAsync;
        public Common.CommandHandler Preview { get; set; }
        public ManifestOutGoingCollection Collection { get; set; }


        public ManifestOutGoingListVM()
        {
            Manifest = new Models.PenjualanManifestView();
            Preview = new CommandHandler { ExecuteAction = x => PreviewAction(), CanExecuteAction = x => PreviewValidation() };
            UpdateDeparture = new CommandHandler { CanExecuteAction = x => UpdateDepartureValidate(), ExecuteAction = UpdateDepartureAction };
            UpdateOrigin = new CommandHandler { CanExecuteAction = x => UpdateOriginValidate(), ExecuteAction = UpdateOriginAction };
            UpdateManifestInfo = new CommandHandler { CanExecuteAction = UpdateManifestInfoValidation, ExecuteAction = UpdateManifestInfoActionAsync };
            PrintTitipanKapal = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = PrintTitipanKapalAction };
            PrintPackingList = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = PrintPackingListAction };
            MainVM.ManifestOutgoingCollection.RefreshCompleted += ManifestOutgoingCollection_RefreshCompleted;
            RefreshAction(null);
        }


        protected override void RefreshAction(object obj)
        {
            ProgressIsActive = true;
            MainVM.ManifestOutgoingCollection.Refresh();
        }


        private void ManifestOutgoingCollection_RefreshCompleted()
        {
            if (ProgressIsActive)
                ProgressIsActive = false;
        }

        private async void PrintPackingListAction(object obj)
        {
            var list = await MainVM.ManifestOutgoingCollection.GetPackingList(SelectedItem.Id);
            var config = HelperPrint.GetReportSetting();
            var sources = new List<Microsoft.Reporting.WinForms.ReportDataSource>() {
                new Microsoft.Reporting.WinForms.ReportDataSource(){ Name="DataSet1", Value=list},
                new Microsoft.Reporting.WinForms.ReportDataSource(){ Name="Config", Value=config}
                };

            var content = new Reports.Contents.ReportContent(sources,
                   "TrireksaApp.Reports.Layouts.PackingListLayout.rdlc", null);
            var dlg = new ModernWindow
            {
                Content = content,
                Title = "Bukti Titipan Kapal",
                Style = (Style)App.Current.Resources["BlankWindow"],
                ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                WindowState = WindowState.Maximized,
            };

            dlg.ShowDialog();

        }

        private void PrintTitipanKapalAction(object obj)
        {
            var item = SelectedItem;
            if(item!=null && item.Information!=null)
            {
                var list = new List<Titipankapal>
                {
                    new Titipankapal{ AgentContactName= item.Agent.ContactName,  AgentName =   item.Agent.Name, AgentHandphone=item.Agent.Handphone, AgentPhone=item.Agent.Phone,
                     ArmadaName=item.Information.ArmadaName, Code = item.Id, CrewAddress= item.Information.Address, Jumlah=item.PackingList.GroupBy(x=>x.PackNumber).Count(), CrewContact=item.Information.Contact,  CrewName=item.Information.CrewName,
                     Destination=item.DestinationNavigation.Name, Origin=item.OriginNavigation.Name, PackNumber=item.PackingList.GroupBy(x=>x.PackNumber).Count(),
                        DestinationCode=item.DestinationNavigation.Code, OriginCode=item.OriginNavigation.Code, PortType=item.PortType, ReferenceNumber=item.Information.ReferenceNumber}
                };

                var config = HelperPrint.GetReportSetting();
                


                var sources = new List<Microsoft.Reporting.WinForms.ReportDataSource>() {
                new Microsoft.Reporting.WinForms.ReportDataSource(){ Name="DataSet1", Value=list},
                new Microsoft.Reporting.WinForms.ReportDataSource(){ Name="Config", Value=config}
                };



                var content = new Reports.Contents.ReportContent(sources,
                  "TrireksaApp.Reports.Layouts.TitipanKapalLayout.rdlc", null);
                var dlg = new ModernWindow
                {
                    Content = content,
                    Title = "Bukti Titipan Kapal",
                    Style = (Style)App.Current.Resources["BlankWindow"],
                    ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                    WindowState = WindowState.Maximized,
                };

                dlg.ShowDialog();
            }else
            {
                ModernDialog.ShowMessage("Data Tidak Ditemukan","Info", MessageBoxButton.OK);
            }

        }

        private async void UpdateManifestInfoActionAsync(object obj)
        {
            UpdateInformationIsAsync = true;
            if (SelectedItem.Information.ManifestId == 0)
                SelectedItem.Information.ManifestId = SelectedItem.Id;
            await MainVM.ManifestOutgoingCollection.UpdateInformation(SelectedItem.Information);
            UpdateInformationIsAsync = false;
        }


        private bool UpdateManifestInfoValidation(object obj)
        {
            if (SelectedItem != null)
            {
                return true;
            }
            else
                return false;
        }

        public bool OnOriginPortIsSync
        {
            get
            {
                return _originISync;

            }
            set
            {
                _originISync = value;
                OnPropertyChanged("OnOriginPortIsSync");

            }
        }
        private bool UpdateOriginValidate()
        {
            if (SelectedItem != null && SelectedItem.OnOriginPort!=null && OnOriginPortIsSync == false)
            {
                return true;
            }
            else
                return false;
        }


       
        private async void UpdateOriginAction(object obj)
        {
            OnOriginPortIsSync = true;
            var res = await MainVM.ManifestOutgoingCollection.UpdateOrigin(SelectedItem);
            if (res)
            {
                OnOriginPortIsSync = false;
            }
            OnOriginPortIsSync = false;
        }

      
        private bool UpdateDepartureValidate()
        {
            if (SelectedItem != null && SelectedItem.OnDestinationPort!=null && OnDestinationPortIsSync== false)
            {
                return true;
            }
            else
                return false;
        }
        private async void UpdateDepartureAction(object obj)
        {
            OnDestinationPortIsSync = true;
            await MainVM.ManifestOutgoingCollection.UpdateDestination(SelectedItem);
            OnDestinationPortIsSync = false;

        }

        

        private bool PreviewValidation()
        {
            return true;
        }

        private void PreviewAction()
        {

            var listSource = new List<Microsoft.Reporting.WinForms.ReportDataSource>();
            var setting = HelperPrint.GetReportSetting();

            setting.FirstOrDefault().SignName = ResourcesBase.User.FullName ?? ResourcesBase.User.UserName;
            listSource.Add(new Microsoft.Reporting.WinForms.ReportDataSource() { Value = Manifest.Source, Name="DataSet1" });
            listSource.Add(new Microsoft.Reporting.WinForms.ReportDataSource() { Value = setting, Name= "Config" });

            var content = new Reports.Contents.ReportContent(listSource,
              "TrireksaApp.Reports.Layouts.ManifestOutgoingLayout.rdlc", null);
            var dlg = new ModernWindow
            {
                Content = content,
                Title = "Manifest Outgoing",
                Style = (Style)App.Current.Resources["BlankWindow"],
                ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                WindowState = WindowState.Maximized,
            };

            dlg.ShowDialog();
        }

       
        public Manifestoutgoing SelectedItem
        {
            get { return _selected; }
            set
            {
                _selected = value;
                if(_selected!=null)
                {
                    if (_selected.Information == null)
                    {
                        _selected.Information = new ManifestInformation { ManifestType = ManifestType.Outgoing, ManifestId = _selected.Id, PortType = _selected.PortType };
                    }
                    Manifest.SetManifest(_selected);
                }
                OnPropertyChanged("SelectedItem");
            }

        }

        public PenjualanManifestView Manifest { get; set; }
        public CommandHandler UpdateDeparture { get; private set; }
        public CommandHandler UpdateOrigin { get; private set; }
        public bool OnDestinationPortIsSync
        {
            get { return _destinationAsyn; }
            set
            {
                _destinationAsyn = value;
                OnPropertyChanged("OnDestinationPortIsSync");
            }
        }

        public CommandHandler UpdateManifestInfo { get; private set; }
        public CommandHandler PrintTitipanKapal { get; }
        public CommandHandler PrintPackingList { get; }

        public bool UpdateInformationIsAsync
        {
            get { return _updateAsync; }
            set
            {
                _updateAsync = value;
                OnPropertyChanged("UpdateInformationIsAsync");
            }
        }
    }
}