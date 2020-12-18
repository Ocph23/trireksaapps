/*using DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{

    public class AgenServices
    {
        private ApplicationDbContext _context;

        public AgenServices(ApplicationDbContext context)
        {
            _context = context;
        }


        public Task<List<Agent>> Get()
        {
            try
            {
                var result = _context.Agents.Include(x => x.CitiesCanAccess)
                .ThenInclude(x => x.City);
                return result.ToListAsync();
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }


        public Task<Agent> GetById(int modelId)
        {
            try
            {
                var result = _context.Agents.Where(x => x.Id == modelId)
                .Include(x => x.CitiesCanAccess).ThenInclude(x => x.City).AsNoTracking();
                return result.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }


        public async Task<Agent> Post(Agent model)
        {
            try
            {
                _context.Agents.Add(model);
                var saveResult = await _context.SaveChangesAsync();
                if (saveResult <= 0)
                    throw new SystemException("Data Berhasil Disimpan !");

                return model;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        public async Task<Agent> Put(int modelId, Agent model)
        {
            try
            {
                var agent = await _context.Agents.Where(x => x.Id == modelId)
                .Include(x => x.CitiesCanAccess).SingleOrDefaultAsync();
                if (agent == null)
                    throw new SystemException("Data Tidak Ditemukan !");


                _context.Entry(agent).CurrentValues.SetValues(model);
                _context.Entry(agent).State = EntityState.Modified;

                foreach (var item in agent.CitiesCanAccess.ToList())
                {
                    var itemExist = model.CitiesCanAccess.Find(x => x.CityId == item.CityId);
                    if (itemExist == null)
                    {
                        _context.Entry(item).State = EntityState.Deleted;
                        agent.CitiesCanAccess.Remove(item);
                    }
                }

                foreach (var item in model.CitiesCanAccess)
                {
                    if (item.AgentId <= 0 && agent.CitiesCanAccess.Where(x => x.CityId == item.CityId).FirstOrDefault() == null)
                    {
                        _context.Entry(item).State = EntityState.Added;
                        agent.CitiesCanAccess.Add(item);
                    }
                }


                var saveResult = await _context.SaveChangesAsync();
                if (saveResult <= 0)
                    throw new SystemException("Data Tidak Berhasil Diubah !");

                return model;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }


        public async Task<bool> Delete(int modelId)
        {
            try
            {
                var agent = await _context.Agents.Where(x => x.Id == modelId).Include(x => x.CitiesCanAccess).SingleOrDefaultAsync();
                if (agent == null)
                    throw new SystemException("Data Tidak Ditemukan !");

                _context.Agents.Remove(agent);
                var saveResult = await _context.SaveChangesAsync();
                if (saveResult <= 0)
                    throw new SystemException("Data Berhasil Dihapus !");

                return true;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }


    }
}
*/