namespace ModelsShared
{
    public class Photo :BaseNotify, IPhoto
    {
        private byte[] _picture;
        private byte[] _thumb;

        public byte[] Picture {
            get { return _picture; }
            set
            {
            SetProperty(ref    _picture , value);
            }
        }
        public byte[] Thumb {

            get { return _thumb; }
            set
            {
            SetProperty(ref    _thumb , value);
            }
        }
        public int Id { get; set; }
        public int STT { get; set; }
        public int PenjualanId { get; set; }
        public string Ext { get; set; }
        public string Path { get; set; }
        public string File { get; set; }
    }



    public interface IPhoto:IPhotoBase
    {
        byte[] Picture { get; set; }

        byte[] Thumb { get; set; }

        int STT { get; set; }
    }

    public interface IPhotoBase
    {
        int Id { get; set; }

        int PenjualanId { get; set; }

        string Ext { get; set; }


        string Path { get; set; }

        string File { get; set; }


    }


}
