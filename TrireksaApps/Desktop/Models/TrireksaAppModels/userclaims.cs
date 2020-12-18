namespace ModelsShared.Models
{
    public class Userclaims : BaseNotify
    {
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public string UserId
        {
            get { return _userid; }
            set
            {
                SetProperty(ref _userid, value);
            }
        }

        public string ClaimType
        {
            get { return _claimtype; }
            set
            {
                SetProperty(ref _claimtype, value);
            }
        }

        public string ClaimValue
        {
            get { return _claimvalue; }
            set
            {
                SetProperty(ref _claimvalue, value);
            }
        }

        private int _id;
        private string _userid;
        private string _claimtype;
        private string _claimvalue;
    }
}


