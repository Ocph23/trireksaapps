
using System;
namespace TrireksaAppContext.ReportModels
{
    public class PackingListPrintModel:BaseNotify
    {
       
        public int ManifestID
        {
            get => _manifestid;
            set => SetProperty(ref _manifestid, value);
        }



        public int PackNumber
        {
            get => _packnumber;
            set => SetProperty(ref _packnumber, value);
        }

        public int CollyNumber
        {
            get => _collynumber;
            set => SetProperty(ref _collynumber, value);
        }

        public int Code
        {
            get => _Code;
            set => SetProperty(ref _Code, value);
        }
       


        private double _Wight;
        public double Weight
        {
            get => _Wight;
            set => SetProperty(ref _Wight, value);
        }



        public int STT
        {
            get => _STT;
            set => SetProperty(ref _STT, value);
        }



        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        
        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        public string Handphone
        {
            get => _handphone;
            set => SetProperty(ref _handphone, value);
        }

        public string Shiper
        {
            get => _Shiper;
            set => SetProperty(ref _Shiper, value);
        }

        public string Reciver
        {
            get => _Reciver;
            set => SetProperty(ref _Reciver, value);
        }

        public string ManifestCode { get; set; }

        public string STTCode { get; set; }

        public DateTime CreatedDate { get; set; }


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
    }
}
