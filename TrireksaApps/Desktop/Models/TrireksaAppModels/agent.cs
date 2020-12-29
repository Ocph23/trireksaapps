using System.Collections.Generic;

namespace ModelsShared.Models
{

    public class Agent : BaseNotify, IModelValidate
    {
        public int Id
        {
            get { return _id; }
            set
            {
            SetProperty(ref    _id , value);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
            SetProperty(ref    _name , value);
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
            SetProperty(ref    _address , value);
            }
        }

        public string NPWP
        {
            get { return _npwp; }
            set
            {
            SetProperty(ref    _npwp , value);
            }
        }


        public string ContactName
        {
            get { return _contactname; }
            set
            {
            SetProperty(ref    _contactname , value);
            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
            SetProperty(ref    _phone , value);
            }
        }

        public string Handphone
        {
            get { return _handphone; }
            set
            {
            SetProperty(ref    _handphone , value);
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
            SetProperty(ref    _email , value);
            }
        }

        public int CityID
        {
            get { return _cityid; }
            set
            {
            SetProperty(ref    _cityid , value);
            }
        }

        public List<CityAgentCanAccess> Cityagentcanaccess { get; set; }

        public bool IsValid
        {
            get
            {
                return ValidatedAction();
            }
        }

        public bool ValidatedAction()
        {
            var result = true;
            if (string.IsNullOrEmpty(this.Address) || string.IsNullOrEmpty(this.ContactName) || CityID <= 0 || string.IsNullOrEmpty(this.Handphone) || string.IsNullOrEmpty(this.Name))
                result = false;

            return result;
        }

        private int _id;
        private string _name;
        private string _address;
        private string _npwp;
        private string _contactname;
        private string _phone;
        private string _handphone;
        private string _email;
        private int _cityid;


    }
}
