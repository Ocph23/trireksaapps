namespace ModelsShared.Models
{
    public class Userrole : BaseNotify
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

        public virtual Roles Role { get; set; }
        public virtual Users User { get; set; }

        private string _roleid;
        private string _userid;
    }
}


