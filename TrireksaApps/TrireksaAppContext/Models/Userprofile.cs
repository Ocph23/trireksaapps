using System;
using System.Collections.Generic;

namespace TrireksaAppContext.Models
{
    public partial class Userprofile
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public byte[] Photo { get; set; }
        public string UserCode { get; set; }

        public virtual Users UserCodeNavigation { get; set; }
    }
}
