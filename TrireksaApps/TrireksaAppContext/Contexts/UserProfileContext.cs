
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using TrireksaAppContext.Models;
using Microsoft.EntityFrameworkCore;

namespace TrireksaAppContext
{
    public class UserProfileContext
    {
        private readonly ApplicationDbContext db;
        public UserProfileContext(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        public Task<IEnumerable<Userprofile>> Get()
        {
            var result = db.Userprofile.Include(x => x.User).ThenInclude(x => x.Userrole).ThenInclude(x => x.Role); 
            return Task.FromResult(result.AsEnumerable());
        }

        public Task<Userprofile> GetProfile(string userid)
        {
            var profile =  db.Userprofile.Include(x => x.User).ThenInclude(x => x.Userrole).ThenInclude(x => x.Role);
            return Task.FromResult(profile.Where(x=>x.User.UserName==userid).FirstOrDefault());
        }

        public async Task<Role> AddNewRole(string userId, string Id)
        {

            try
            {
                Userrole userrole = new Userrole{ RoleId = Id, UserId = userId };
                var role = db.Role.Where(O => O.Id == Id).FirstOrDefault();
                if (role != null)
                {
                    db.Userrole.Add(userrole);
                   await db.SaveChangesAsync();
                    return role;
                }
                else
                {
                    throw new SystemException("Role Tidak Ditemukan");
                }

            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<bool> RemoveRole(string userId, string roleId)
        {

            try
            {
                Userrole userrole = db.Userrole.Where(x=>x.RoleId == roleId && x.UserId == userId ).FirstOrDefault();
                if (userrole != null)
                {
                    db.Userrole.Remove(userrole);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new SystemException("Role Tidak Ditemukan");
                }

            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public Task<bool> Post(Userprofile t)
        {
            return Task.FromResult(false);
        }


        public async Task<bool> Put(int id, Userprofile value)
        {
            try
            {
                var profile = db.Userprofile.Where(x => x.Id ==id).FirstOrDefault();
                if (profile != null)
                {
                    db.Entry(profile).CurrentValues.SetValues(value);
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch 
            {
                throw new SystemException(MessageCollection.Message(MessageType.UpdateFaild));
            }

         

        }
    }
}
