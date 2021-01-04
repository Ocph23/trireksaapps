using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TrireksaAppContext.Models;

namespace TrireksaAppContext
{

    public class PortsContext
    {
        private readonly ApplicationDbContext db;

        public PortsContext( ApplicationDbContext _db)
        {
            db = _db;
        }


        public Task<IEnumerable<Port>> Get()
        {
            var result = db.Port.Include(x => x.City);
            return Task.FromResult(result.AsEnumerable());
        }

        public Task< Port> Get(int id)
        {
            var result = db.Port.Include(x=>x.City).Where(O => O.Id == id).FirstOrDefault();
            return Task.FromResult( result);
        }

        public async Task<Port> Post(Port value)
        {
            db.Port.Add(value);
            if(await db.SaveChangesAsync()<=0)
                throw new SystemException(MessageCollection.Message(MessageType.SaveFail));
            return value;
        }

        public async Task<bool> Put(int id, Port value)
        {
            var existsData = await Get(id);
            if (existsData == null)
                throw new SystemException("Data Not Found !");

            db.Entry(existsData).CurrentValues.SetValues(value);
            if (await db.SaveChangesAsync() <= 0)
                throw new SystemException("Data Not Saved !");

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var existsData = await Get(id);
            if (existsData == null)
                throw new SystemException("Data Not Found !");

            db.Port.Remove(existsData);
            if (await db.SaveChangesAsync() <= 0)
                throw new SystemException("Data Not Saved !");

            return true;
        }
    }
}
