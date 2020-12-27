

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaAppContext.Models;

namespace TrireksaAppContext
{
    public class ShipsContext
    {
        private readonly ApplicationDbContext db;

        public ShipsContext(ApplicationDbContext _db)
        {
            db = _db;
        }
        // GET: api/Ships
        public async Task<bool> Delete(int id)
        {
            var existsShip = await GetById(id);
            if (existsShip == null)
                throw new SystemException("Data Not Found !");

            db.Ships.Remove(existsShip);
            if (await db.SaveChangesAsync() > 0)
            {
                return true;
            }
            else
                throw new SystemException(MessageCollection.Message(MessageType.UpdateFaild));
        }

        public Task<IEnumerable<Ships>> Get()
        {
            return Task.FromResult(db.Ships.AsEnumerable());
        }


        public Task<Ships> GetById(int Id)
        {
            return Task.FromResult(db.Ships.Where(x=>x.Id==Id).FirstOrDefault());
        }

        public async Task<Ships> InsertAndGetItem(Ships t)
        {
            db.Ships.Add(t);
            if (await db.SaveChangesAsync()>0)
                return t;
            else
                throw new SystemException(MessageCollection.Message(MessageType.SaveFail));
        }

        public async Task<Ships> UpdateAndGetItem(int id, Ships t)
        {
            var existsShip = await GetById(id);
            if (existsShip == null)
                throw new SystemException("Data Not Found !");

            db.Entry(existsShip).CurrentValues.SetValues(t);
            if (await db.SaveChangesAsync()>0)
            {
                return t;
            }
            else
                throw new SystemException(MessageCollection.Message(MessageType.UpdateFaild));
        }
    }
}
