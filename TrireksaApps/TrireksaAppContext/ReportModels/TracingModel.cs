using System;
using System.Collections.Generic;


namespace TrireksaAppContext.Models
{
    public class TracingModel : BaseNotify
    {
        public List<Manifestoutgoing> Manifests { get; set; }

        private string _shiperName;

        public string ShiperName
        {
            get { return _shiperName; }
            set { _shiperName = value; }
        }

        private string _reciverName;

        public string ReciverName
        {
            get { return _reciverName; }
            set { _reciverName = value; }
        }

        private string _reciveName;

        public string ReciveName
        {
            get { return _reciveName; }
            set { _reciveName = value; }
        }


        private DateTime _reciveDate;
        public DateTime ReciveDate
        {
            get { return _reciveDate; }
            set { _reciveDate = value; }
        }

        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }


        private string _PortOriginName;
        public string PortOriginName
        {
            get { return _PortOriginName; }
            set { _PortOriginName = value; }
        }

        private string _PortDestinationName;
        public string PortDestinationName
        {
            get { return _PortDestinationName; }
            set { _PortDestinationName = value; }
        }
        private string _PortDestionationCode;
        public string PortDestionationCode
        {
            get { return _PortDestionationCode; }
            set { _PortDestionationCode = value; }
        }


        private string _PortOriginCode;
        public string PortOriginCode
        {
            get { return _PortOriginCode; }
            set { _PortOriginCode = value; }
        }

        public int STT
        {
            get { return _stt; }
            set
            {
               SetProperty(ref _stt , value);
            }
        }


        public int ShiperID
        {
            get { return _shiperid; }
            set
            {
                SetProperty(ref _shiperid, value);
            }
        }

        public int ReciverID
        {
            get { return _reciverid; }
            set
            {
                SetProperty(ref _reciverid, value);
            }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                SetProperty(ref _price, value);
            }
        }

        public DateTime? ChangeDate
        {
            get { return _changedate; }
            set
            {
                SetProperty(ref _changedate, value);
            }
        }

        public DateTime UpdateDate
        {
            get { return _updatedate; }
            set
            {
                SetProperty(ref _updatedate , value);
            }
        }

        public string UserID
        {
            get { return _userid; }
            set
            {
                SetProperty(ref _userid, value);
            }
        }

        public TypeOfWeight TypeOfWeight
        {
            get { return _typeofweight; }
            set
            {
                SetProperty(ref _typeofweight, value);
            }
        }

        public virtual PayType PayType
        {
            get { return _paytype; }
            set
            {
                SetProperty(ref _paytype, value);
            }
        }

        public virtual PortType PortType
        {
            get { return _portType; }
            set
            {
                SetProperty(ref _portType, value);
            }
        }
        public int CityID
        {
            get { return _cityid; }
            set
            {
                SetProperty(ref _cityid, value);
            }
        }

        //   [DbColumn("CustomerIsPay")]
        public virtual CustomerIsPay CustomerIsPay
        {
            get { return _customerispay; }
            set
            {
                SetProperty(ref _customerispay, value);
            }
        }

        public int CustomerIdIsPay
        {
            get
            {
                if (CustomerIsPay == CustomerIsPay.Reciver)
                    return this.ReciverID;
                else
                    return this.ShiperID;
            }
            set
            {
                _customerIdIsPay = value;
                if (value == this.ReciverID)
                    CustomerIsPay = CustomerIsPay.Reciver;
                if (value == this.ShiperID)
                    CustomerIsPay = CustomerIsPay.Shiper;

            }
        }



        public double PackingCosts
        {
            get { return _packingcosts; }
            set
            {
                SetProperty(ref _packingcosts, value);
            }
        }

        public double Tax
        {
            get { return _tax; }
            set
            {
                SetProperty(ref _tax, value);
            }
        }

        public double Etc
        {
            get { return _etc; }
            set
            {
                SetProperty(ref _etc, value);
            }
        }


        private bool _actived;

        public bool Actived
        {
            get { return _actived; }
            set
            {
                SetProperty(ref _actived , value);

            }
        }

        public bool IsPaid
        {
            get { return _isPaid; }
            set
            {
                SetProperty(ref _isPaid, value);
            }
        }


        private string _content;
        public string Content
        {
            get { return _content; }
            set
            {
                SetProperty(ref _content, value);
            }
        }


        private string _do;
        public string DoNumber
        {
            get { return _do; }
            set { SetProperty(ref _do, value); }
        }

        private string _note;

        public string Note
        {
            get { return _note; }
            set { SetProperty(ref _note, value); }
        }

        
        public double Total
        {
            get
            {
                if (Weight > 0 && Pcs> 0)
                {
                    double berat = 0;
                    berat = Weight;
                    var biaya = (berat * this.Price) + this.PackingCosts + this.Etc;
                    var tax = biaya * (this.Tax / 100);
                    _total = biaya + tax;
                }
                return _total;
            }
            set
            {

                SetProperty(ref _total, value);
            }


        }

  
     
        private int _pcs;
        public int Pcs
        {
            get
            {
                return _pcs;
            }
            set
            {
                SetProperty(ref _pcs, value);
            }
        }


        private double _weight;
        public double Weight
        {
            get
            {
                return _weight;
            }
            set { SetProperty(ref _weight, value); }
        }

        public int Id {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        private int _stt;
        private int _shiperid;
        private int _reciverid;
        private double _price;
        private DateTime? _changedate;
        private DateTime _updatedate;
        private string _userid;
        private TypeOfWeight _typeofweight;
        private PayType _paytype;
        private CustomerIsPay _customerispay;
        private double _packingcosts;
        private double _tax;
        private double _etc;
        private double _total;
        private PortType _portType;
        private int _cityid;
        private bool _isPaid;
        private int _customerIdIsPay;
        private int _id;



    }
}