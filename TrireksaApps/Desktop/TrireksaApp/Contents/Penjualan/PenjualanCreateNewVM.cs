using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ModelsShared.Models;
using FirstFloor.ModernUI.Windows.Controls;
using TrireksaApp.Common;
using System.Windows.Data;
using ModelsShared;
using System.Collections.ObjectModel;
using System.Windows;

namespace TrireksaApp.Contents.Penjualan
{
    public class PenjualanCreateVM : BaseNotify
    {
        public MainWindowVM MainVM { get; private set; }
        public AppConfiguration AppConfig { get; }
        public List<ModelsShared.Models.TypeOfWeight> TypeOfWeights { get; set; }
        public List<ModelsShared.Models.PortType> PortTypes { get; set; }
        public List<ModelsShared.Models.PayType> PayTypes { get; set; }
        public bool IsSearch { get; set; }

        //collection
        public ObservableCollection<ModelsShared.Models.City> CitiesSource { get; private set; }

        public CustomerControl ShiperControl { get; set; }
        public CustomerControl RecieverControl { get; }
        public CustomerControl WillPayControl { get; }

        public PenjualanCreateVM()
        {
            this.MainVM = ResourcesBase.GetMainWindowViewModel();
            this.AppConfig = MainVM.SystemConfiguration;
            this.TypeOfWeights = ResourcesBase.GetEnumCollection<TypeOfWeight>();
            this.PortTypes = ResourcesBase.GetEnumCollection<PortType>();
            this.PayTypes = ResourcesBase.GetEnumCollection<PayType>();

            STTModel = new STTCreateModel(0)
            {
                ChangeDate = DateTime.Now
            };

           // ShiperControl=new CustomerControl();

            CitiesSource = new ObservableCollection<ModelsShared.Models.City>(MainVM.CityCollection.Source);

            this.Origins = (CollectionView)CollectionViewSource.GetDefaultView(CitiesSource);
            this.Destinations = (CollectionView)CollectionViewSource.GetDefaultView(CitiesSource);
            Save = new CommandHandler { CanExecuteAction = x => SaveValidation(), ExecuteAction = SaveAction };
            Print = new CommandHandler { CanExecuteAction = x => PrintSelected != null, ExecuteAction = x => PrintAction() };
            PrintWithForm = new CommandHandler { CanExecuteAction = x => PrintSelected != null, ExecuteAction = x => PrintFormAction() };
            SaveAndPrint = new CommandHandler { CanExecuteAction = x => SaveValidation(), ExecuteAction = x => SaveAndPrintAction() };
            Cancel = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => CancalAction() };
            Search = new CommandHandler { ExecuteAction = SearchAction };
            ChangeWeight = new CommandHandler { ExecuteAction = x => ChangeWeightAction() };
            PriceUpdate = new CommandHandler { CanExecuteAction = PriceUpdateValidation, ExecuteAction = PriceUpdateAction };
            CreateNewSTT();
        }

        private STTCreateModel stt;

        public STTCreateModel STTModel
        {
            get { return stt; }
            set
            {
                SetProperty(ref stt, value);
            }
        }


        private async void CreateNewSTT()
        {
            var STTnumber = await MainVM.PenjualanCollection.NewSTT();
            if (STTnumber <= 0)
                STTnumber = 1;
            STTModel = new STTCreateModel(STTnumber)
            {
                ChangeDate = DateTime.Now
            };
            STTModel.OnChangePrice += STTModel_OnChangePrice;
            RefreshSource();

        }

        private async void RefreshSource()
        {
            await Task.Delay(1000);

            if(MainVM.CustomerCollection.Source==null || MainVM.CustomerCollection.Source.Count<=0)
                MainVM.CustomerCollection.Refresh();
        }

        private void STTModel_OnChangePrice(bool isOld)
        {
            if(!isOld)
                this.GetPrices();
        }
               

        #region action

        private void PrintFormAction()
        {
            Helper.PrintPreviewNotaAction(PrintSelected);
        }


        private async void CancalAction()
        {
            this.ClearForms();
            await Task.Delay(1000);
            CreateNewSTT();
        }

        private async void SearchAction(object obj)
        {
            try
            {
                RefreshSource();
                var stt = Convert.ToInt32(obj);
                var result = await MainVM.PenjualanCollection.GetItemBySTT(stt);
                if (result != null)
                {
                    var newmodel = new STTCreateModel(result);
                    STTModel = newmodel;
                    STTModel.OnChangePrice += STTModel_OnChangePrice;
                }
                   
                else
                {
                    STTModel = new STTCreateModel(stt);
                    throw new SystemException("Data Tidak Ditemukan !");
                }
                  
                await Task.Delay(1000);
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK);
            }

        }

       

        private void SaveAndPrintAction()
        {
            try
            {
                if (Save.CanExecute(null))
                {
                    Save.Execute(true);
                    Helper.PrintNotaAction(STTModel);
                }
                 


            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }

        }

        private void PrintAction()
        {
            Helper.PrintNotaAction(PrintSelected);
        }

        private async void SaveAction(object param)
        {
            try
            {
                await STTModel.Save();
                CreateNewSTT();
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "Error", MessageBoxButton.OK);
            }
        }
      

        private void PriceUpdateAction(object obj)
        {

            try
            {
                this.PriceIsSync = true;
               // var data = GetPriceObject();
                var context = new PricesContext(STTModel);
                context.UpdatePrice(STTModel.Price);
            }
            catch (Exception ex)
            {

                ModernDialog.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK);
            }
            this.PriceIsSync = false;
        }

        private void ChangeWeightAction()
        {
            try
            {
                if (STTModel.Colly == null)
                    STTModel.Colly = new List<Colly>();
                using (var vm = new Contents.Penjualan.AddCollyVM(STTModel.TypeOfWeight, STTModel.Colly))
                {
                    if (STTModel.TypeOfWeight != TypeOfWeight.None)
                    {
                        var form = new Contents.Penjualan.AddColly
                        {
                            DataContext = vm
                        };
                        form.ShowDialog();

                    }
                }
                // this.Details = vm.Source;

                if (STTModel.Colly != null && STTModel.Colly.Count > 0)
                {
                    STTModel.SetTotal();
                    DetailsIsEmpty = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Validate

        private bool PriceUpdateValidation(object obj)
        {
            if (STTModel!=null && STTModel.Shiper != null && STTModel.Reciver != null && STTModel.Price > 0 && STTModel.PortType != PortType.None &&
                 STTModel.CustomerIsPay != CustomerIsPay.None && STTModel.PayType != PayType.None)
            {
                return true;
            }
            return false;
        }


        //private bool SetWeightValidation()
        //{
        //    if (STTModel.ShiperID > 0 && STTModel.ReciverID > 0)
        //    {
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        private bool SaveValidation()
        {
            if (STTModel != null)
                return STTModel.IsValid;
            return false;

        }
        #endregion

        #region Fields
        private bool _detailsIsempty;
        private bool _priceIsSync;
        private ModelsShared.Models.Customer _WillPaySelected;
        #endregion

        #region Properties
        public CommandHandler Save { get; set; }
        public CommandHandler SaveAndPrint { get; set; }
        public CommandHandler Print { get; set; }
        public CommandHandler PrintWithForm { get; }
        public CommandHandler Cancel { get; set; }
        public CommandHandler Search { get; set; }
        public CollectionView Origins { get; set; }
        public CollectionView Destinations { get; set; }
        public CommandHandler ChangeWeight { get; private set; }
        public CommandHandler PriceUpdate { get; private set; }

        public ModelsShared.Models.Penjualan PrintSelected { get; private set; }
       
        public ModelsShared.Models.Customer WillPaySelected
        {
            get { return _WillPaySelected; }
            set
            {
            SetProperty(ref    _WillPaySelected , value);
                if (value != null)
                {
                    STTModel.CustomerIdIsPay = value.Id;
                }
            }
        }

        #endregion

        public bool PriceIsSync
        {
            get
            {
                return _priceIsSync;
            }
            set
            {
              SetProperty(ref  _priceIsSync , value);
            }
        }



        internal void SetWeight(object sender)
        {
            var value = (TypeOfWeight)sender;

            if (STTModel.Colly == null)
                STTModel.Colly = new List<Colly>();

            using (var vm = new Contents.Penjualan.AddCollyVM(value, STTModel.Colly))
            {

                if (value != TypeOfWeight.None)
                {
                    var form = new Contents.Penjualan.AddColly
                    {
                        DataContext = vm
                    };

                    var dlg = new ModernDialog
                    {
                        Title = "Add Item",
                        Content = form
                    };
                    dlg.ShowDialog();

                }
            }
            STTModel.SetTotal();
            // this.Details = vm.Source;

            if (STTModel.Colly != null && STTModel.Colly.Count > 0)
            {
                STTModel.TypeOfWeight = value;
                DetailsIsEmpty = true;
            }
            STTModel.TypeOfWeight = value;
        }

        public bool DetailsIsEmpty
        {
            get
            {
                if (STTModel.TypeOfWeight == TypeOfWeight.None)
                {
                    _detailsIsempty = false;
                }

                return _detailsIsempty;
            }
            set
            {
               SetProperty(ref _detailsIsempty , value);
            }
        }



        private  void ClearForms()
        {
            STTModel.Shiper = null;
            STTModel.Reciver = null;
            //ShiperControl.SearchText=string.Empty;
            //RecieverControl.SearchText = string.Empty; 
            //WillPayControl.SearchText = string.Empty; ;
            DetailsIsEmpty = false;
        }

        private void GetPrices()
        {
            if (STTModel != null)
            {
                var pricesContext = new PricesContext(STTModel);
                pricesContext.GetPrices().ContinueWith(GetPricesAsyncHandler);
            }
        }

        private async void GetPricesAsyncHandler(Task<double> obj)
        {
            var price = await obj;
            if ( price > 0)
            {
                STTModel.Price = price;
            }
            else
            {
                STTModel.Price = 0;
            }

        }
    }


    public delegate void ChangePrice (bool isOld);
    public class STTCreateModel : ModelsShared.Models.Penjualan, IDataErrorInfo
    {
        public event ChangePrice OnChangePrice;
        public STTCreateModel(int sTTnumber)
        {
            STT = sTTnumber;

        }
        private bool IsOld { get; set; }

        public STTCreateModel(ModelsShared.Models.Penjualan result)
        {
            IsOld = true;
            if (result != null)
            {
                
                Actived = result.Actived;
                ChangeDate = result.ChangeDate;
                FromCity = result.FromCity;
                ToCity = result.ToCity;
                Content = result.Content;
                CustomerIdIsPay = result.CustomerIdIsPay;
                CustomerIsPay = result.CustomerIsPay;
                DeliveryStatus = result.DeliveryStatus;
                Colly = result.Colly;
                DoNumber = result.DoNumber;
                Etc = result.Etc;
                Id = result.Id;
                Note = result.Note;
                PackingCosts = result.PackingCosts;
                PayType = result.PayType;
                Pcs = result.Pcs;
                PortType = result.PortType;
                Price = result.Price;
                ShiperID = result.ShiperID;
                ReciverID = result.ReciverID;
                Shiper = result.Shiper;
                Reciver = result.Reciver;
                STT = result.STT;
                TypeOfWeight = result.TypeOfWeight;
                Tax = result.Tax;
                UpdateDate = result.UpdateDate;
                UserID = result.UserID;
                IsPaid = result.IsPaid;
            }
        }

        internal async Task Save()
        {
            var MainVM = ResourcesBase.GetMainWindowViewModel();
            try
            {
                this.UpdateDate = DateTime.Now;
                if (this.Id <= 0)
                {
                    this.Actived = true;
                    var result = await MainVM.PenjualanCollection.Add(
                        new ModelsShared.Models.Penjualan
                        {
                            Actived = Actived,
                            ChangeDate = ChangeDate,
                            FromCity = FromCity,
                            ToCity = ToCity,
                            Content = Content,
                            CustomerIdIsPay = CustomerIdIsPay,
                            CustomerIsPay = CustomerIsPay,
                            DeliveryStatus = DeliveryStatus,
                            Colly = Colly,
                            DoNumber = DoNumber,
                            Etc = Etc,
                            Id = Id,
                            Note = Note,
                            PackingCosts = PackingCosts,
                            PayType = PayType,
                            Pcs = Pcs,
                            PortType = PortType,
                            Price = Price,
                            ShiperID = ShiperID,
                            ReciverID = ReciverID,
                            Shiper = Shiper,
                            Reciver = Reciver,
                            STT = STT,
                            TypeOfWeight = TypeOfWeight,
                            Tax = Tax,
                            UpdateDate = UpdateDate,
                            UserID = UserID,
                            IsPaid = IsPaid
                        });
                    if (result != null)
                    {
                        ModernDialog.ShowMessage("Data Is Saved !", "Information", System.Windows.MessageBoxButton.OK);
                    }
                    else
                    {
                        throw new SystemException("Data Not Saved !");
                    }
                }
                else
                {
                    if (Id > 0 && Colly != null && Colly.Count > 0)
                    {
                        foreach (var dataItem in Colly)
                        {
                            dataItem.PenjualanId = Id;
                        }
                    }

                    var result = await MainVM.PenjualanCollection.Update(Id, this);
                    if (result)
                    {

                        MainVM.PenjualanCollection.SourceView.Refresh();
                        ModernDialog.ShowMessage("Data Is Saved !", "Information", System.Windows.MessageBoxButton.OK);
                    }
                    else
                    {
                        throw new SystemException("Data Not Saved !");
                    }
                }

            }
            catch (Exception ex)
            {

                ModernDialog.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK);
            }

        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "ShiperID")
                {
                    if (ShiperID <= 0)
                        return "Select Shiper";
                    else if (ShiperID == ReciverID)
                        return "Shiper & Reciver Same";

                    return null;
                }

                if (columnName == "ReciverID")
                {
                    if (ReciverID <= 0)
                        return "Select Reciver";
                    else if (ShiperID == ReciverID)
                        return "Shiper & Reciver Same";

                    return null;
                }


                if (columnName == "CustomerIdIsPay")
                {
                    if (CustomerIdIsPay <= 0)
                        return "Select Customer";
                    return null;
                }

                if (columnName == "Price")
                    return (Price <= 0) ? "Price Requerid" : null;

                if (columnName == "TypeOfWeight")
                {
                    return (TypeOfWeight == TypeOfWeight.None) ? "Select Type" : null;
                }


                if (columnName == "PayType")
                    return (PayType == PayType.None) ? "Select a Payment Type" : null;


                if (columnName == "PortType")
                    return (PortType == PortType.None) ? "Select Port Type" : null;


                if (columnName == "From")
                    return (FromCity <= 0) ? "Select Origin City" : null;

                if (columnName == "To")
                    return (ToCity <= 0) ? "Select Destination City" : null;


                if (columnName == "CustomerIsPay")
                    return (CustomerIsPay == CustomerIsPay.None) ? "Who Will Pay ?" : null;


                if (columnName == "Content")
                    return string.IsNullOrEmpty(Content) ? "Content Required ?" : null;


                return null;
            }
        }

        public string Error
        {
            get; set;
        }
        public bool IsValid
        {
            get
            {

                if (this.CustomerIsPay == CustomerIsPay.None)
                    return false;

                if (this.CustomerIsPay == CustomerIsPay.Other && (CustomerIdIsPay == ShiperID || CustomerIdIsPay == ReciverID))
                    return false;

                if (this.FromCity == 0 || this.Price == 0 || this.ToCity==0)
                    return false;

                if (this.Colly == null || this.Colly.Count == 0)
                    return false;

                if (PortType == PortType.None)
                    return false;
                if (this.ReciverID == 0 && this.ShiperID == 0)
                    return false;
                if (this.TypeOfWeight == TypeOfWeight.None)
                    return false;
                if (this.PayType == PayType.None)
                    return false;
                if (string.IsNullOrEmpty(Content))
                    return false;
                

                return true;



            }
        }

        public override CustomerIsPay CustomerIsPay {
            get => base.CustomerIsPay;
            set {
                base.CustomerIsPay = value;
                if (value == CustomerIsPay.Shiper)
                    CustomerIdIsPay = ShiperID;
                if (value == CustomerIsPay.Reciver)
                    CustomerIdIsPay = ReciverID;
                OnChangePrice?.Invoke(IsOld);
                ChangeOldStatus();
            } 
        }

        private async void ChangeOldStatus()
        {
            await Task.Delay(2000);
            if (IsOld)
                IsOld = false;
        }

        public override PayType PayType { get => base.PayType;
            set { base.PayType = value;
                OnChangePrice?.Invoke(IsOld);
                ChangeOldStatus();
            }
        }

        public override PortType PortType { get => base.PortType;
            set
            {
                base.PortType= value;
                OnChangePrice?.Invoke(IsOld);
                ChangeOldStatus();
            }
        }



    }


    public class PricesContext
    {
        private readonly STTCreateModel sTTModel;

        public PricesContext(STTCreateModel sTTModel)
        {
            this.sTTModel = sTTModel;
        }

        public bool PricesParamaterValid
        {
            get
            {
                if (sTTModel.Reciver!=null && sTTModel.CustomerIdIsPay>0 && 
                    sTTModel.PayType != PayType.None && sTTModel.PortType != PortType.None && sTTModel.Reciver.CityID>0)
                    return true;
                else
                    return false;
            }


        }

        public Task<double> GetPrices()
        {
            if (PricesParamaterValid)
            {
                var prices = GetModel();
                var context = ResourcesBase.GetMainWindowViewModel().PricesCollection;
                return context.GetPricesByCustomer(prices);
            }
            return Task.FromResult((double)0);
        }

        private Prices GetModel()
        {
            var prices = new Prices {  FromCity= sTTModel.Shiper.CityID, PayType= sTTModel.PayType, PortType= sTTModel .PortType,
             PriceValue=sTTModel.Price, ShiperId= sTTModel.CustomerIdIsPay, ToCity= sTTModel.Reciver.CityID};
            return prices;
        }

        public async void UpdatePrice(double price)
        {
            if (PricesParamaterValid)
            {
                var prices = GetModel();
                prices.PriceValue = price;
                var context = ResourcesBase.GetMainWindowViewModel().PricesCollection;
                await context.SetPrices(prices);
            }
        }
    }
}