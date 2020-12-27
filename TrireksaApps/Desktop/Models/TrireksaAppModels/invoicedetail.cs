using System;


namespace ModelsShared.Models
{
    public delegate void SetTotal();

    //public class Invoicedetail:BaseNotify
    //{
    //    public event SetTotal TotalAction;

    //    public int Id
    //    {
    //        get { return _id; }
    //        set
    //        {
    //            SetProperty(ref _id , value);
    //        }
    //    }

    //    public int PenjualanId
    //    {
    //        get { return _penjualanId; }
    //        set
    //        {
    //            SetProperty(ref _penjualanId , value);
    //        }
    //    }

    //    public int InvoiceId
    //    {
    //        get { return _invId; }
    //        set
    //        {
    //            SetProperty(ref _invId , value);
    //        }
    //    }



    //    private string _reciver;

    //    public string Reciver
    //    {
    //        get { return _reciver; }
    //        set 
    //        {
    //            SetProperty(ref _reciver, value);
    //        }
    //    }

    //    private string _shiper;

    //    public string Shiper
    //    {
    //        get { return _shiper; }
    //        set 
    //        {
    //            SetProperty(ref _shiper, value);
    //        }
    //    }

    //    private int _pcs;

    //    public int Pcs
    //    {
    //        get { return _pcs; }
    //        set 
    //        {
    //            SetProperty(ref _pcs, value);
    //        }
    //    }

    //    private double _weight;

    //    public double Weight
    //    {
    //        get { return _weight; }
    //        set
    //        {
    //            SetProperty(ref _weight , value);
    //        }
    //    }

    //    private double _price;

    //    public double Price
    //    {
    //        get { return _price; }
    //        set
    //        {
    //            SetProperty(ref _price , value);
    //        }
    //    }
    //    private double _total;

    //    public double Total
    //    {
    //        get
    //        {
    //            double berat = Weight;
    //            var biaya = (berat * this.Price) + this.PackingCosts + this.Etc;
    //                var tax = biaya * (this.Tax / 100);
    //                _total = biaya + tax;
                
    //            return _total;
    //        }
    //        set
    //        {
    //            SetProperty(ref _total , value);
    //        }


    //    }

    //    private DateTime changeDate;

    //    public DateTime ChangeDate
    //    {
    //        get { return changeDate; }
    //        set 
    //        {
    //            SetProperty(ref changeDate, value);
    //        }
    //    }



    //    private bool _isSelected;

    //    public bool IsSelected
    //    {
    //        get { return _isSelected; }
    //        set 
    //        {
    //            SetProperty(ref _isSelected, value);
    //            if (TotalAction != null)
    //                TotalAction.Invoke();
    //        }
    //    }


    //    public double PackingCosts { get; set; }
    //    public double Etc { get; set; }
    //    public double Tax { get; set; }
    //    public int STT { get; set; }
    //    public string DoNumber { get; set; }
    //    public string Tujuan { get;  set; }
    //    public PortType PortType { get; set; }
    //    public virtual string Via {
    //        get
    //        {
    //            switch (PortType)
    //            {
    //                case PortType.Sea:
    //                    return "Laut";
    //                case PortType.Air:
    //                    return "Udara";
    //                case PortType.Land:
    //                    return "Darat";
    //                default:
    //                    return string.Empty;
    //            }
    //        }
    //        set 
    //        {
    //            SetProperty(ref _via, value);
    //        }
    //    }

    //    private int _id;
    //    private int _invId;
    //    private int _penjualanId;
    //    private string _via;
    //}



    public partial class Invoicedetail :BaseNotify
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int PenjualanId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Penjualan Penjualan { get; set; }


        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                SetProperty(ref _isSelected, value);
            }
        }
    }




}
