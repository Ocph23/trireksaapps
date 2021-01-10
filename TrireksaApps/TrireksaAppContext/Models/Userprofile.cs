using System;
using System.Collections.Generic;

namespace TrireksaAppContext.Models
{
    public partial class Userprofile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public byte[] Photo { get; set; }
        public string UserId { get; set; }
        public virtual Users User { get; set; }
    }
}
