using TrireksaMobile;

namespace Accounts
{
    public class Role : BaseNotify
    {
        public string Id
        {
            get { return _id; }
            set
            {
              SetProperty(ref  _id , value);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
               SetProperty(ref _name , value);
            }
        }

        private string _id;
        private string _name;
    }
}


