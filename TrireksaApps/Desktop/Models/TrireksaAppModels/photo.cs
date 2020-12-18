
namespace ModelsShared.Models
{
    public class Photo : BaseNotify, IPhotoBase
    {
        public int Id
        {
            get { return _id; }
            set
            {
            SetProperty(ref    _id , value);
            }
        }

        public int PenjualanId
        {
            get { return _PenjualanId; }
            set
            {
            SetProperty(ref    _PenjualanId , value);
            }
        }

        public string Ext
        {
            get { return _ext; }
            set
            {
            SetProperty(ref    _ext , value);
            }
        }

        public string Path
        {
            get { return _Path; }
            set
            {
            SetProperty(ref    _Path , value);
            }
        }

        public string File
        {
            get { return _File; }
            set
            {
            SetProperty(ref    _File , value);
            }
        }
        private int _id;
        private int _PenjualanId;
        private string _ext;
        private string _Path;
        private string _File;
    }
}
