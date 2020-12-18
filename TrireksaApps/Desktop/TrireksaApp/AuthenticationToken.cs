using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaApp
{
    public class AuthenticationToken
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public ICollection<string> Roles { get; set; }
       
    }


}
