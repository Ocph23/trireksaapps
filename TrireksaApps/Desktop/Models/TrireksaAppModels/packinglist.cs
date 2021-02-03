
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

        public int ManifestId
        {
            get { return _manifestid; }
            set
            {
                SetProperty(ref _manifestid, value);
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

        public virtual Colly Colly { get; set; }
        public virtual Manifestoutgoing Manifest { get; set; }


        public int STT
        {
            get { return _stt; }
            set
            {
                SetProperty(ref _stt, value);
            }
        }


        public double Weight { get; set; }

        private int _id;
        private int _manifestid;
        private int _stt;
        private int _packnumber;
        private int _collynumber;
        private int _penjualanId;
        private int _collyId;
    }
}


