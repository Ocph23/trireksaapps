namespace ModelsShared.Models
{
    public class Customer : BaseNotify,IModelValidate
    {
          public int Id 
          {
            get => _id;
            set => SetProperty(ref _id, value);
          } 

          public string Name 
          {
            get => _name;
            set => SetProperty(ref _name, value);
        } 

          public CustomerType CustomerType 
          {
            get => _customertype;
            set => SetProperty(ref _customertype, value);
        } 

          public string ContactName 
          {
            get => _contactname;
            set => SetProperty(ref _contactname, value);
        } 

          public string Phone1 
          {
            get => _phone1;
            set => SetProperty(ref _phone1, value);
        }

        public string Phone2
        {
            get => _phone2;
            set => SetProperty(ref _phone2, value);
        }

        public string Handphone
        {
            get => _handphone;
            set => SetProperty(ref _handphone, value);
        }

          public string Address 
          {
            get => _address;
            set => SetProperty(ref _address, value);
        }


        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public int CityID
        {
            get => _cityId;
            set => SetProperty(ref _cityId, value);
        }

        public bool IsValid
        {
            get
            {
                return ValidatedAction();
            }
        }

        public City City { get;  set; }

        public bool ValidatedAction()
        {
            bool result = true;
            if (this.CustomerType == CustomerType.None || this.CityID == 0 || string.IsNullOrEmpty(this.Name) || 
                string.IsNullOrEmpty(this.ContactName) || string.IsNullOrEmpty(this.Address))
            {
                result = false;
            }

            return result;

        }

      

        private int _id;
        private string _name;
        private CustomerType _customertype;
        private string _contactname;
        private string _phone1;
        private string _address;
        private string _phone2;
        private string _handphone;
        private string _email;
        private int _cityId;
    }
}


