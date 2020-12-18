namespace ModelsShared.Models
{
    public class Packinglist : BaseNotify
    {
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public int ManifestID
        {
            get { return _manifestid; }
            set
            {
                SetProperty(ref _manifestid, value);
            }
        }

        public int STT
        {
            get { return _stt; }
            set
            {
                SetProperty(ref _stt, value);
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

        public int CollyId
        {
            get { return _collyId; }
            set
            {
                SetProperty(ref _collyId, value);
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

        private int _id;
        private int _manifestid;
        private int _stt;
        private int _packnumber;
        private int _collynumber;
        private int _penjualanId;
        private int _collyId;
    }
}


