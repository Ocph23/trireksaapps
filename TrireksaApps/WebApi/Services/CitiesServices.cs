/*using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{

    public class CitiesServices
    {
        private ApplicationDbContext _context;

        public CitiesServices(ApplicationDbContext context)
        {
            _context = context;
        }


        public Task<List<City>> Get()
        {
            try
            {
                var result = _context.Cities;
                return result.ToListAsync();
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }


        public Task<City> GetById(int modelId)
        {
            try
            {
                var result = _context.Cities.Where(x => x.Id == modelId);
                return result.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }


        public async Task<City> Post(City model)
        {
            try
            {
                _context.Cities.Add(model);
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

        public async Task<City> Put(int modelId, City model)
        {
            try
            {
                var dbModel = await _context.Cities.Where(x => x.Id == modelId).SingleOrDefaultAsync();
                if (dbModel == null)
                    throw new SystemException("Data Tidak Ditemukan !");


                _context.Entry(dbModel).CurrentValues.SetValues(model);
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


        public async Task<bool> Delete(int modelId)
        {
            try
            {
                var dbModel = await _context.Cities.Where(x => x.Id == modelId).SingleOrDefaultAsync();
                if (dbModel == null)
                    throw new SystemException("Data Tidak Ditemukan !");

                _context.Cities.Remove(dbModel);
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