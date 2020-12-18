using System;
using System.Collections.Generic;

namespace TrireksaAppContext.Models
{
    public partial class Userrole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Users User { get; set; }
    }
}
