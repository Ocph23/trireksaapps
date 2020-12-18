namespace ModelsShared.Models
{
    public class Userrole : BaseNotify
    {
        public int RoleId
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

        private int _roleid;
        private string _userid;
    }
}


