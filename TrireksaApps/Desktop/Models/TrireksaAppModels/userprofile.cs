using System.Collections.Generic;

namespace ModelsShared.Models
{

    public class Userprofile : User
    {


        public int UserId
        {
            get { return _userid; }
            set
            {
                SetProperty(ref _userid, value);
            }
        }


        public string UserCode
        {
            get { return _userCode; }
            set
            {
                SetProperty(ref _userCode, value);
            }
        }


        public string FirstName
        {
            get { return _firstname; }
            set
            {
                SetProperty(ref _firstname, value);
            }
        }

        public string LastName
        {
            get { return _lastname; }
            set
            {
                SetProperty(ref _lastname, value);
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                SetProperty(ref _address, value);
            }
        }

        public byte[] Photo
        {
            get { return _photo; }
            set
            {
                SetProperty(ref _photo, value);
            }
        }

        public List<Roles> Roles { get; set; }

        private int _userid;
        private string _firstname;
        private string _lastname;
        private string _address;
        private byte[] _photo;
        private string _userCode;
    }
}