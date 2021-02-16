using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TrireksaAppContext.Models;

namespace TrireksaAppContext
{
    public class AppVersionContext
    {
        private readonly ApplicationDbContext db;

        public AppVersionContext(ApplicationDbContext _db)
        {
            db = _db;
        }

        public Task<IEnumerable<AppVersion>> Get()
        {
            var result = db.AppVersion.AsEnumerable();
            return Task.FromResult(result);
        }

        // GET: api/AppVersions/5

        public Task<AppVersion> Get(int id)
        {
            return Task.FromResult(db.AppVersion.Where(O => O.Id == id).FirstOrDefault());
        }

        public Task<AppVersion> GetLast()
        {

            var result = db.AppVersion.AsEnumerable();
            return Task.FromResult(result.Last());
        }

        // POST: api/AppVersions
        public Task<AppVersion> Post(AppVersion value)
        {
            try
            {
                db.AppVersion.Add(value);
                db.SaveChanges();
                return Task.FromResult(value);
            }
            catch (SqlException ex)
            {
                throw new SystemException(ex.Message);
            }

        }

        // PUT: api/AppVersions/5
        public async Task<bool> Put(int id, AppVersion value)
        {
            try
            {

                var existsData = await db.AppVersion.Where(x=>x.Id==id).FirstAsync();
                if (existsData == null)
                    throw new SystemException("Data Not Found !");

                //update parent 
                db.Entry(existsData).CurrentValues.SetValues(value);

                //delete childs
                
                db.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {

                throw new SystemException(ex.Message);
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        // DELETE: api/AppVersions/5
       
    }
}
