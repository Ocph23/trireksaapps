using System;
using System.Collections.Generic;



namespace ModelsShared.Models
{
    public class Invoice:BaseNotify
    {
        public int Id
        {
            get { return _id; }
            set
            {
               SetProperty(ref _id , value);
            }
        }

        public int Number
        {
            get { return _number; }
            set
            {
               SetProperty(ref _number , value);
                NumberView = ModelHelpers.GenerateInvoiceCode(Number, this.CreateDate);
            }
        }

        public string NumberView
        {
            get {
                return _numberView; }
            set
            {
               SetProperty(ref _numberView , value);
            }
        }


        public int CustomerId
        {
            get { return _customerId; }
            set
            {
               SetProperty(ref _customerId , value);
            }
        }

        public bool IsDelivery
        {
            get { return _isdelivery; }
            set
            {
               SetProperty(ref _isdelivery , value);
            }
        }

        public InvoiceStatus InvoiceStatus
        {
            get { return _invoicestatus; }
            set
            {
               SetProperty(ref _invoicestatus , value);
            }
        }

        public DateTime? DeliveryDate
        {
            get { return _deliverydate; }
            set
            {
               SetProperty(ref _deliverydate , value);
            }
        }

        public string ReciverBy
        {
            get { return _reciverby; }
            set
            {
               SetProperty(ref _reciverby , value);
            }
        }

        public DateTime? ReciveDate
        {
            get { return _recivedate; }
            set
            {
               SetProperty(ref _recivedate , value);
            }
        }

        public DateTime DeadLine
        {
            get { return _tempodate; }
            set
            {
               SetProperty(ref _tempodate , value);
            }
        }

        public InvoicePayType InvoicePayType
        {
            get { return _invoicepaytype; }
            set
            {
               SetProperty(ref _invoicepaytype , value);
            }
        }

        public DateTime CreateDate
        {
            get { return _createdate; }
            set
            {
               SetProperty(ref _createdate , value);
                NumberView = ModelHelpers.GenerateInvoiceCode(Number, value);
            }
        }

        public int UserId
        {
            get { return _userid; }
            set
            {
               SetProperty(ref _userid , value);
            }
        }

        public string  CustomerName
        {
            get
            {
                return _customerName;
            }
            set
            {
               SetProperty(ref _customerName , value);
            }
        }

        public double Tax { get; set; }
        public double Biaya { get; set; }

        public virtual double Total
        {
            get { _total= Biaya + (Biaya * (Tax / 100)); return _total; }

            set { SetProperty(ref _total , value); }
        }

        public List<Invoicedetail> Details
        {
            get
            {
                if(_details==null)
                {
                    _details = new List<Invoicedetail>();
                }

                return _details;
            }

            set
            {
               SetProperty(ref _details , value);
            }
        }


        private DateTime? _paidDate;
        public DateTime? PaidDate
        {
            get { return _paidDate; }
            set { 
              SetProperty(ref  _paidDate , value);
            }
        }



        private int _id;
        private int _number;
        private bool _isdelivery;
        private InvoiceStatus _invoicestatus;
        private DateTime? _deliverydate;
        private string _reciverby;
        private DateTime? _recivedate;
        private DateTime _tempodate;
        private InvoicePayType _invoicepaytype;
        private DateTime _createdate;
        private int _userid;
        private int _customerId;
        private string _customerName;
        private List<Invoicedetail> _details;
        private string _numberView;
        private double _total;
    }
}

