using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrireksaAppContext.Models;

namespace TrireksaAppContext
{
    public class CitiesAgentCanAccessContext
    {
        private ApplicationDbContext db;

        public CitiesAgentCanAccessContext(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        public Task<IEnumerable<Cityagentcanaccess>> Get(int id)
        {
            return Task.FromResult(db.Cityagentcanaccess.Where(O => O.AgentId== id).AsEnumerable());
        }

        // POST: api/CitiesAgentCanAccess
        public async Task<int> Post(Cityagentcanaccess value)
        {
            db.Cityagentcanaccess.Add(value);
            if (await db.SaveChangesAsync() <= 0)
                throw new SystemException("Data Not Saved !");

            return value.Id;
        }


        public async Task<bool> Delete(int id)
        {
            var exsitsData = db.Cityagentcanaccess.Where(O => O.Id == id).FirstOrDefault();
            if (exsitsData == null)
                throw new SystemException("Data Not Found !");

            db.Cityagentcanaccess.Remove(exsitsData);
            if (await db.SaveChangesAsync() <= 0)
                throw new SystemException("Data Not Saved !");
            return true;
        }

        public async Task<Cityagentcanaccess> OnChangeItemTrue(Cityagentcanaccess obj)
        {
            try
            {
                db.Cityagentcanaccess.Add(obj);
                if (await db.SaveChangesAsync() <= 0)
                        throw new SystemException(MessageCollection.Message(MessageType.SaveFail));
                return obj;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<Cityagentcanaccess> OnChangeItemFalse(Cityagentcanaccess obj)
        {
            try
            {
                var exsitsData = db.Cityagentcanaccess.Where(O => O.Id == obj.Id).FirstOrDefault();
                if (exsitsData == null)
                    throw new SystemException("Data Not Found !");

                db.Cityagentcanaccess.Remove(exsitsData);
                if (await db.SaveChangesAsync() <= 0)
                    throw new SystemException("Data Not Saved !");
                return obj;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }

        }

    }
}

