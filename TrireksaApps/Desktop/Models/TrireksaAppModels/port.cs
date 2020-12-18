namespace ModelsShared.Models
{
    public class Port : BaseNotify, IModelValidate
    {
        public int Id
        {
            get { return _id; }
            set
            {
               SetProperty(ref _id , value);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
               SetProperty(ref _name , value);
            }
        }

        public PortType PortType
        {
            get { return _porttype; }
            set
            {
               SetProperty(ref _porttype , value);
            }
        }


        public string Code
        {
            get { return _code; }
            set
            {
               SetProperty(ref _code , value);
            }
        }

        public int CityID
        {
            get { return _cityId; }
            set
            {
               SetProperty(ref _cityId , value);
            }
        }


        // Tambahan

        public string CityName
        {
            get { return _cityName; }
            set
            {
               SetProperty(ref _cityName , value);
            }
        }

        public bool IsValid
        {
            get
            {
                return ValidatedAction();
            }
        }

        public bool ValidatedAction()
        {
            bool result = true;
            if (CityID <= 0 || string.IsNullOrEmpty(this.Code) ||
                string.IsNullOrEmpty(this.Name) || this.PortType == PortType.None)
            {
                result = false;

            }

            return result;

        }

        private int _id;
        private string _name;
        private PortType _porttype;
        private string _code;
        private int _cityId;
        private string _cityName;


    }
}


