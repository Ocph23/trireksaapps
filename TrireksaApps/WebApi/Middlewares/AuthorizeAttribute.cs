using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TrireksaAppContext.Models;


namespace WebApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (Users)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            if (!string.IsNullOrEmpty(Roles))
            {
                var splite = Roles.Split(",");
                var found = false;
                foreach (var item in splite)
                {
                    var role = item.ToLower().Trim();
                    if (user.UserInRole(role))
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }

        public string Roles { get; set; }

    }


    public static class UserExtention
    {
        public static bool UserInRole(this Users user, string roleName)
        {
           try
           {
                if(user==null)
                    return false;

                if(user.Userrole.Where(x=>x.Role.Name.ToLower()==roleName.ToLower()).Count()>0)
                    return true;
                return false;
           }
           catch 
           {
                return false;
           }
       }
    }
}