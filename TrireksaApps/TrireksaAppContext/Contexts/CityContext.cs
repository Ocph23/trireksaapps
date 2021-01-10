using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TrireksaAppContext.Models;

namespace TrireksaAppContext
{
    public class CityContext
    {

        private readonly ApplicationDbContext db;

        //SignalR
      //  private readonly IHubContext<TrireksaAppHub> _hubContext;
        public CityContext(ApplicationDbContext _dbContext)
        {
            //_hubContext = hubContext;
            db = _dbContext;
        }


        public IEnumerable<City> Get()
        {
            return db.City.ToList();
        }

        // GET: api/City/5
        public Task<City> Get(int id)
        {
            return Task.FromResult(db.City.Where(O => O.Id == id).FirstOrDefault());
        }

        // POST: api/City
        public async Task<City> Post(City value)
        {
            db.City.Add(value);
            if (await db.SaveChangesAsync() <= 0)
                throw new SystemException("Data Not Saved !");
            return value;

        }



        public async Task<City> Put(int id, City value)
        {
            var existsdata = db.City.Where(x => x.Id == id).FirstOrDefault();
            if (existsdata == null)
                throw new SystemException("Data Not Found !");

            db.Entry(existsdata).CurrentValues.SetValues(value);
            if (await db.SaveChangesAsync() <= 0)
                throw new SystemException("Data Not Saved !");
            return value;


        }

        // DELETE: api/City/5
        public async Task<bool> Delete(int id)
        {
            var existsdata = db.City.Where(x => x.Id == id).FirstOrDefault();
            if (existsdata == null)
                throw new SystemException("Data Not Found !");

            db.City.Remove(existsdata);
            if (await db.SaveChangesAsync() <= 0)
                throw new SystemException("Data Not Saved !");
            return true;
        }
    }
}
