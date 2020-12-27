using System;

namespace ModelsShared.Models
{
    public class Deliverystatus:BaseNotify
    {
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        public int PenjualanId
        {
            get { return _penjualanId; }
            set
            {
                SetProperty(ref _penjualanId, value);
            }
        }

        public DateTime? ReciveDate
        {
            get { return _recivedate; }
            set
            {
                SetProperty(ref _recivedate, value);
            }
        }

        public string ReciveName
        {
            get { return _recivename; }
            set
            {
                SetProperty(ref _recivename, value);
            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                SetProperty(ref _phone, value);
            }
        }

        public bool IsSignIn
        {
            get { return _issignin; }
            set
            {
                SetProperty(ref _issignin, value);
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                SetProperty(ref _description, value);
            }
        }

        public string UserID
        {
            get { return _userid; }
            set
            {
              SetProperty(ref  _userid , value);
            }
        }

        private int _id;
        private DateTime? _recivedate;
        private string _recivename;
        private string _phone;
        private bool _issignin;
        private string _description;
        private string _userid;
        private int _penjualanId;
    }

}
