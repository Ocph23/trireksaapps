using System;
using System.Collections.Generic;

namespace TrireksaAppContext.Models
{
    public partial class Role
    {
        public Role()
        {
            Userrole = new HashSet<Userrole>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Userrole> Userrole { get; set; }
    }
}
