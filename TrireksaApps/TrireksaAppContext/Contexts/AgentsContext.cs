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
    public class AgentsContext
    {
        private readonly IHubContext<TrireksaAppHub> _hubContext;
        private readonly ApplicationDbContext db;

        public AgentsContext(IHubContext<TrireksaAppHub> hubContext, ApplicationDbContext _db)
        {
            _hubContext = hubContext;
            db = _db;
        }


        public Task<IEnumerable<Agent>> Get()
        {
            var result = db.Agent.Include(x => x.Cityagentcanaccess);
            return Task.FromResult(result.AsEnumerable());
        }

        // GET: api/Agents/5

        public Task<Agent> Get(int id)
        {
            return Task.FromResult(db.Agent.Where(O => O.Id == id).FirstOrDefault());
        }

        // POST: api/Agents
        public async Task<Agent> Post(Agent value)
        {
            try
            {
                db.Agent.Add(value);
                db.SaveChanges();
                await _hubContext.Clients.All.SendAsync("agent", "post", value);
                return value;
            }
            catch (SqlException ex)
            {
                throw new SystemException(ex.Message);
            }

        }

        // PUT: api/Agents/5
        public async Task<bool> Put(int id, Agent value)
        {
            try
            {

                var updatedCity = true;
                if (value.Cityagentcanaccess == null)
                {
                    updatedCity = false;
                    value.Cityagentcanaccess = new List<Cityagentcanaccess>();
                }

                var existsData = await db.Agent.Where(x=>x.Id==id).Include(x=>x.Cityagentcanaccess).FirstAsync();
                if (existsData == null)
                    throw new SystemException("Data Not Found !");

                //update parent 
                db.Entry(existsData).CurrentValues.SetValues(value);

                //delete childs

                if (value.Cityagentcanaccess != null && updatedCity)
                {



                    foreach (var existingChild in existsData.Cityagentcanaccess)
                    {
                        if (!value.Cityagentcanaccess.Any(c => c.Id == existingChild.Id))
                            db.Cityagentcanaccess.Remove(existingChild);
                    }


                    foreach (var childModel in value.Cityagentcanaccess)
                    {
                        var existingChild = existsData.Cityagentcanaccess
                            .Where(c => c.Id == childModel.Id)
                            .SingleOrDefault();

                        if (existingChild != null)
                            db.Entry(existingChild).CurrentValues.SetValues(childModel);
                        else
                        {
                            existsData.Cityagentcanaccess.Add(childModel);
                        }
                    }
                }
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

        // DELETE: api/Agents/5
        public Task<bool> Delete(int id)
        {
            try
            {
                var dataExist = db.Agent.Where(x => x.Id == id).FirstOrDefault();
                if (dataExist != null)
                {
                    db.Agent.Remove(dataExist);
                    return Task.FromResult(true);
                }
                throw new SystemException("Data Not Found !");
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }
    }
}
