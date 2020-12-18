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
        public static async Task<bool> Initialize(ApplicationDbContext context, IUserService userService)
        {
            context.Database.EnsureCreated();
            if (!context.Role.Any())
            {

                context.Role.Add(new Role { Name = "Administrator" });
                context.Role.Add(new Role { Name = "Manager" });
                context.Role.Add(new Role { Name = "Admin" });
                context.Role.Add(new Role { Name = "Accounting" });
                context.Role.Add(new Role { Name = "Operational" });
                context.Role.Add(new Role { Name = "Employee" });
                context.SaveChanges();
            }


            if (!context.Users.Any())
            {
                try
                {
                    var administrator = new Models.RegisterModel { UserName = "Administrator", Password = "Sony@77" };
                    await userService.Register(administrator);
                    var admin = new Models.RegisterModel { UserName = "Admin", Password = "Sony@77" };
                    await userService.Register(admin);
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