using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsShared.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Userrole> Roles { get; set; }
    }
}
