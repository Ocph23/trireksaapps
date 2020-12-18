using System;

namespace ModelsShared.Models
{
    public class PackingListPrintModel : BaseNotify
    {

        public int ManifestID
        {
            get { return _manifestid; }
            set
            {
                SetProperty(ref _manifestid, value);
            }
        }



        public int PackNumber
        {
            get { return _packnumber; }
            set
            {
                SetProperty(ref _packnumber, value);
            }
        }

        public int CollyNumber
        {
            get { return _collynumber; }
            set
            {
                SetProperty(ref _collynumber, value);
            }
        }

        public int Code
        {
            get { return _Code; }
            set
            {
                SetProperty(ref _Code, value);
            }
        }



        private double _Wight;
        public double Weight
        {
            get
            {
                return _Wight;
            }
            set
            {
                SetProperty(ref _Wight, value);
            }
        }



        public int STT
        {
            get { return _STT; }
            set
            {
                SetProperty(ref _STT, value);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
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

        public string Handphone
        {
            get { return _handphone; }
            set
            {
                SetProperty(ref _handphone, value);
            }
        }

        public string Shiper
        {
            get { return _Shiper; }
            set
            {
                SetProperty(ref _Shiper, value);
            }
        }

        public string Reciver
        {
            get { return _Reciver; }
            set
            {
                SetProperty(ref _Reciver, value);
            }
        }

        public string ManifestCode
        {
            get
            {
                if (this.Code > 0)
                    return ModelHelpers.GenerateManifestOutGoingCode(Code, this.CreatedDate);
                else
                    return string.Empty;
            }
        }

        public string STTCode
        {
            get
            {
                return string.Format("{0:D5}", STT);
            }
        }

        public DateTime CreatedDate
        {
            get { return _createddate; }
            set
            {
                SetProperty(ref _createddate, value);
            }
        }


        private string _name;
        private string _phone;
        private string _handphone;
        private string _Reciver;
        private string _Shiper;
        private int _manifestid;
        private int _packnumber;
        private int _collynumber;
        private int _STT;
        private int _Code;
        private DateTime _createddate;
    }
}
