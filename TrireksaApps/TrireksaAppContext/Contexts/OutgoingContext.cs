using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using TrireksaAppContext.Models;
using Microsoft.EntityFrameworkCore;
using TrireksaAppContext.ReportModels;

namespace TrireksaAppContext
{
    public class OutgoingContext
    {
        private const string Sql = "CALL GetDocumentByName @p0";
        private ApplicationDbContext db;

        public OutgoingContext(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        public IEnumerable<Manifestoutgoing> Get()
        {
            var results = db.Manifestoutgoing
                .Include(x => x.Agent)
                .Include(x => x.DestinationNavigation)
                .Include(x => x.OriginNavigation)
                .Include(x => x.Packinglist)
                .Include(x => x.Information);
            return results;
        }


        public IEnumerable<Manifestoutgoing> GetByMount(int month)
        {
             var results = db.Manifestoutgoing.Where(O => O.CreatedDate.Value.Month == month)
                .Include(x => x.Agent)
                .Include(x => x.DestinationNavigation)
                .Include(x => x.OriginNavigation)
                .Include(x => x.Packinglist)
                .Include(x => x.Information);
            return results;
        }

        public async Task<bool> UpdateInformation(Manifestinformation obj)
        {
            if (obj.Id == 0)
            {
                var info = db.Manifestinformation.Where(O => O.ManifestId == obj.ManifestId).FirstOrDefault();
                if (info == null)
                    db.Manifestinformation.Add(obj);
                else
                {
                    db.Entry(obj).CurrentValues.SetValues(obj);
                }

                var result = await db.SaveChangesAsync();
                if (result <= 0)
                    throw new SystemException("Data Not Saved !");
            }
            return true;
        }

        public Task<Manifestoutgoing> Get(int Id)
        {
            var results = db.Manifestoutgoing.Where(x=>x.Id==Id)
                       .Include(x => x.Agent)
                       .Include(x => x.DestinationNavigation)
                       .Include(x => x.OriginNavigation)
                       .Include(x => x.Packinglist)
                       .Include(x => x.Information);
            return Task.FromResult(results.FirstOrDefault());
        }



        public async Task<Manifestoutgoing> InsertAndGetItem(Manifestoutgoing model)
        {
            var transaction = db.Database.BeginTransaction();
            try
            {
                if(!db.Manifestoutgoing.Any())
                    model.Code = 1 ;
                else
                {
                    var last = db.Manifestoutgoing.Max(x=>x.Code);
                    model.Code = last+1;
                }


                var date = DateTime.Now;
                model.CreatedDate = date;
                model.UpdateDate = date;

                db.Manifestoutgoing.Add(model);
                var result = await db.SaveChangesAsync();
                if (result <= 0)
                    throw new SystemException("Data Not Saved !");
                transaction.Commit();

                return model;
              
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new SystemException(ex.Message);
            }
        }

        public async Task<Manifestinformation> InsertInformation(Manifestinformation obj)
        {
            try
            {
                if (obj.Id <= 0)
                    db.Manifestinformation.Add(obj);
                else
                    db.Entry(obj).CurrentValues.SetValues(obj);

                var result = await db.SaveChangesAsync();
                if(result<=0)
                    throw new SystemException("Data Not Saved !");
                return obj;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public Task<IEnumerable<PackingListPrintModel>> GetPackingList(int ManifestId)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        public Task<TitipanKapal> GetTitipanKapal(int ManifestId)
        {
            try
            {

                return null;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        public IEnumerable<Manifestoutgoing> ManifestsByPenjualanId(int penjualanId)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }


        public async Task<Manifestoutgoing> UpdateOrigin(int id, Manifestoutgoing manifest)
        {
            var existsData = db.Manifestoutgoing.SingleOrDefault(x => x.Id == id);
            db.Entry(existsData).CurrentValues.SetValues(manifest);
            var result =await db.SaveChangesAsync();
            if (result>0)
                return manifest;
            else
                throw new SystemException("Data Tidak tersimpan");
        }

        public async Task<Manifestoutgoing> UpdateDestination(int id, Manifestoutgoing manifest)
        {
            var existsData = db.Manifestoutgoing.SingleOrDefault(x => x.Id == id);
            db.Entry(existsData).CurrentValues.SetValues(manifest);
            var result = await db.SaveChangesAsync();
            if (result > 0)
                return manifest;
            else
                throw new SystemException("Data Tidak tersimpan");
        }
    }
}
