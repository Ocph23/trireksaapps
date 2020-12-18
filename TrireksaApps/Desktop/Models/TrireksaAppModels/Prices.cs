namespace ModelsShared.Models
{
    public class Prices : BaseNotify
    {
        private int _id;
        private int _customerid;
        private int _from;
        private int _to;
        private double _price;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        public int ShiperId
        {
            get { return _customerid; }
            set
            {
                _customerid = value;
            }
        }

        private PortType _portType;

        public PortType PortType
        {
            get { return _portType; }
            set
            {
                _portType = value;
            }
        }


        private PayType _payType;
        public PayType PayType
        {
            get { return _payType; }
            set
            {
                _payType = value;
            }
        }


        public int FromCity
        {
            get { return _from; }
            set
            {
                _from = value;
            }
        }

        public int ToCity
        {
            get { return _to; }
            set
            {
                _to = value;
            }
        }

        public double PriceValue
        {
            get { return _price; }
            set
            {
                _price = value;
            }
        }
    }
}
