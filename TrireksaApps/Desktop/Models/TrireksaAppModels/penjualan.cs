using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ModelsShared.Models
{
    public class Penjualan : BaseNotify
    {
        public int Id
        {
            get { return _id; }
            set
            {
            SetProperty(ref    _id , value);
            }
        }

        public int STT
        {
            get { return _stt; }
            set
            {
            SetProperty(ref    _stt , value);

            }
        }


        public int ShiperID
        {
            get { return _shiperid; }
            set
            {
            SetProperty(ref    _shiperid , value);
                if (CustomerIsPay == CustomerIsPay.Shiper)
                    CustomerIdIsPay = value;
                if (Shiper != null && FromCity <= 0)
                    FromCity = Shiper.CityID;
            }
        }

        public int ReciverID
        {
            get { return _reciverid; }
            set
            {
            SetProperty(ref    _reciverid , value);
                if (CustomerIsPay == CustomerIsPay.Reciver)
                    CustomerIdIsPay = value;
                if (Reciver != null && ToCity <= 0)
                    ToCity = Reciver.CityID;
            }
        }

        public double Price
        {
            get { return _price; }
            set
            {
            SetProperty(ref    _price , value);
                SetTotal();
            }
        }

        public DateTime ChangeDate
        {
            get { return _changedate; }
            set
            {
            SetProperty(ref    _changedate , value);
            }
        }

        public DateTime UpdateDate
        {
            get { return _updatedate; }
            set
            {
            SetProperty(ref    _updatedate , value);
            }
        }

        public int UserID
        {
            get { return _userid; }
            set
            {
            SetProperty(ref    _userid , value);
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public TypeOfWeight TypeOfWeight
        {
            get { return _typeofweight; }
            set
            {
            SetProperty(ref    _typeofweight , value);
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public virtual PayType PayType
        {
            get { return _paytype; }
            set
            {
            SetProperty(ref    _paytype , value);
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public virtual PortType PortType
        {
            get { return _portType; }
            set
            {
            SetProperty(ref    _portType , value);
            }
        }
        public int FromCity
        {
            get { return _fromCity; }
            set
            {
            SetProperty(ref    _fromCity , value);
            }
        }

        public int ToCity
        {
            get { return _toCity; }
            set
            {
            SetProperty(ref    _toCity , value);
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public virtual CustomerIsPay CustomerIsPay
        {
            get { return _customerispay; }
            set
            {
            SetProperty(ref    _customerispay , value);
            }
        }

        public int CustomerIdIsPay
        {
            get
            {
                return _customerIdIsPay;
            }

            set
            {
            SetProperty(ref    _customerIdIsPay , value);
                if (value == ShiperID)
                    _customerispay = CustomerIsPay.Shiper;
                else if (value == ReciverID)
                    _customerispay = CustomerIsPay.Reciver;
                else if (value > 0)
                    _customerispay = CustomerIsPay.Other;
                else
                    _customerispay = CustomerIsPay.Shiper;

            }
        }



        public double PackingCosts
        {
            get { return _packingcosts; }
            set
            {
            SetProperty(ref    _packingcosts , value);
                SetTotal();
            }
        }

        public double Tax
        {
            get { return _tax; }
            set
            {
            SetProperty(ref    _tax , value);
                SetTotal();
            }
        }

        public double Etc
        {
            get { return _etc; }
            set
            {
            SetProperty(ref    _etc , value);
                SetTotal();
            }
        }


        private bool _actived;

        public bool Actived
        {
            get { return _actived; }
            set
            {
            SetProperty(ref    _actived , value);

            }
        }

        public bool IsPaid
        {
            get { return _isPaid; }
            set
            {
            SetProperty(ref    _isPaid , value);
            }
        }


        private string _content;
        public string Content
        {
            get { return _content; }
            set
            {
            SetProperty(ref    _content , value);
            }
        }


        private string _do;
        public string DoNumber
        {
            get { return _do; }
            set
            {
            SetProperty(ref    _do , value);
            }
        }

        private string _note;

        public string Note
        {
            get { return _note; }
            set
            {
            SetProperty(ref    _note , value);
            }
        }



        public List<Colly> Details { get; set; }

        public double Total
        {
            get
            {

                if (Details != null && Details.Count > 0)
                {
                    double berat = 0;
                    berat = Details.Sum(O => O.Weight);
                    var biaya = (berat * this.Price) + this.PackingCosts + this.Etc;
                    var tax = biaya * (this.Tax / 100);
                    _total = biaya + tax;
                }
                return _total;
            }
            set
            {
            SetProperty(ref    _total , value);
            }


        }

        public void SetTotal()
        {
            if (Details != null && Details.Count > 0)
            {

                double berat = 0;
                berat = Details.Sum(O => O.Weight);
                Weight = berat;
                Pcs = Details.Count;
                var biaya = (berat * this.Price) + this.PackingCosts + this.Etc;
                var tax = biaya * (this.Tax / 100);
                Total = biaya + tax;
            }
        }


        public Customer Reciver
        {
            get { return _reciever; }
            set
            {
            SetProperty(ref    _reciever , value);
            }
        }
        public Customer Shiper
        {
            get { return _shiper; }
            set
            {
            SetProperty(ref    _shiper , value);
            }
        }

        public Deliverystatus DeliveryStatus { get; set; }

        private int _pcs;

        public int Pcs
        {
            get
            {

                if (this.Details != null && this.Details.Count > 0)
                {
                    _pcs = Details.Count;
                }

                return _pcs;
            }
            set
            {
            SetProperty(ref    _pcs , value);
            }
        }


        private double _weight;

        public double Weight
        {
            get
            {

                if (this.Details != null && this.Details.Count > 0)
                {
                    _weight = Details.Sum<ModelsShared.Models.Colly>(O => O.Weight);
                }

                return _weight;
            }
            set
            {
            SetProperty(ref    _weight , value);
            }
        }


        private int _stt;
        private int _shiperid;
        private int _reciverid;
        private double _price;
        private DateTime _changedate;
        private DateTime _updatedate;
        private int _userid;
        private TypeOfWeight _typeofweight;
        private PayType _paytype;
        private CustomerIsPay _customerispay;
        private double _packingcosts;
        private double _tax;
        private double _etc;
        private double _total;
        private PortType _portType;
        private int _fromCity;
        private bool _isPaid;
        private int _customerIdIsPay;
        private int _id;
        private Customer _reciever;
        private Customer _shiper;
        private int _toCity;
    }

}


