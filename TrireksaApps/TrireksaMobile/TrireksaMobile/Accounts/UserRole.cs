using TrireksaMobile;

namespace Accounts
{
    public class UserRole : BaseNotify
    {
        public string RoleId
        {
            get { return _roleid; }
            set
            {
                SetProperty(ref _roleid, value);
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

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }

        private string _roleid;
        private string _userid;
    }
}


