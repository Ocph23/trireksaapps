
using System.Collections.Generic;
using System.Linq;
using TrireksaAppContext.Models;

namespace WebApi.Models
{
    public class AuthenticateResponse
    {

        public AuthenticateResponse(Users user, string token)
        {
            this.UserName = user.UserName;
            this.FullName = user.FullName;
            this.Email = user.Email;
            this.Token = token;
           // this.Roles = user.Roles.Select(x => x.Role.Name);
        }

        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; }
        public IEnumerable<string> Roles { get; set; }
    }
}