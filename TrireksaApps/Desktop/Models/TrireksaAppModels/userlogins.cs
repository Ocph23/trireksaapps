namespace ModelsShared.Models
{
    public class Userlogins : BaseNotify
    {
        public string LoginProvider
        {
            get { return _loginprovider; }
            set
            {
            SetProperty(ref    _loginprovider , value);
            }
        }

        public string ProviderKey
        {
            get { return _providerkey; }
            set
            {
            SetProperty(ref    _providerkey , value);
            }
        }

        public string UserId
        {
            get { return _userid; }
            set
            {
            SetProperty(ref    _userid , value);
            }
        }

        private string _loginprovider;
        private string _providerkey;
        private string _userid;
    }
}


