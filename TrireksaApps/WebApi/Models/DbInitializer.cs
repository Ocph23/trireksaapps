using System.Threading.Tasks;
using System.Linq;
using WebApi.Middlewares;
using System.Collections.Generic;
using TrireksaAppContext;
using TrireksaAppContext.Models;

namespace WebApi
{
    public class DbInitializer
    {
        public static async Task<bool> Initialize(ApplicationDbContext context,  IUserService userService)
        {
            context.Database.EnsureCreated();
            if (!context.Role.Any())
            {
                context.Role.Add(new Role { Id="1", Name = "Administrator" });
                context.Role.Add(new Role { Id="2", Name = "Manager" });
                context.Role.Add(new Role { Id="3", Name = "Admin" });
                context.Role.Add(new Role { Id="4", Name = "Accounting" });
                context.Role.Add(new Role { Id="5", Name = "Operational" });
                context.Role.Add(new Role { Id="6", Name = "Employee" });
                context.SaveChanges();
            }


            if (!context.Users.Any())
            {
                try
                {
                    var administratorModel = new Models.RegisterModel { UserName = "Administrator", Password = "Sony@77" };
                   var administrator= await userService.Register(administratorModel);
                    await userService.AddToRole(administrator, "Administrator");

                }
                catch (System.Exception ex)
                {
                    throw new System.Exception(ex.Message);
                }
            }

            return true;

        }
    }
}