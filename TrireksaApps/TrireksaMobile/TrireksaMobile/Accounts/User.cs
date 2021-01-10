using System.Collections.Generic;

namespace Accounts
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public bool Activated { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

    }
}


