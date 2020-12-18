using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TrireksaAppContext.Models;

namespace TrireksaAppContext
{
    public class InvoicesContext
    {

        private readonly ApplicationDbContext db;

        public InvoicesContext(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        // GET: api/Invoices
        public async Task<bool> Delete(int id)
        {
            try
            {
                var existsModel = db.Invoices.Where(x => x.Id == id).FirstOrDefault();
                db.Invoices.Remove(existsModel);
                var result =await db.SaveChangesAsync();
                    if(result<=0)
                throw new SystemException(MessageCollection.Message(MessageType.DeleteFail));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public Task<IEnumerable<Invoices>> Get(DateTime start, DateTime end)
        {
            var results = db.Invoices.Where(x => x.CreateDate.Value == start && x.CreateDate.Value == end)
                .Include(x => x.Invoicedetail);
            return Task.FromResult(results.AsEnumerable());
        }


        public Task<Invoices> Get(int Id)
        {
            try
            {
                var inv = db.Invoices.Where(O => O.Id == Id)
                    .Include(x => x.Invoicedetail).FirstOrDefault();
                return Task.FromResult(inv);

            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }



        public async Task<Invoices> InsertAndGetItem(Invoices t)
        {
            try
            {
                db.Invoices.Add(t);
                if (await db.SaveChangesAsync() <= 0)
                    throw new SystemException("Data Not Saved");
                return t;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);


            }
        }


        public async Task<Invoices> UpdateDeliveryDataAction(int Id, Invoices t)
        {
            var existsData = db.Invoices.Where(x => x.Id == Id).FirstOrDefault();
            if (existsData == null)
                throw new SystemException("Data Not Found !");

            db.Entry(t).CurrentValues.SetValues(t);

            if (await db.SaveChangesAsync() <= 0)
                throw new SystemException("Data Not Saved");
            return t;
        }


        public async Task<Invoices> UpdateInvoiceStatusAction(int Id, Invoices t)
        {
            var existsData = await Get(Id);
            if (existsData == null)
                throw new SystemException("Data Not Found !");
            db.Entry(t).CurrentValues.SetValues(t);
            if (await db.SaveChangesAsync() <= 0)
                throw new SystemException("Data Not Saved");

            return existsData;
        }


        public Task<Invoices> GetInvoiceForPenjualanInfo(int Id)
        {
            Invoices inv;

            var det = db.Invoicedetail.Where(O => O.PenjualanId == Id).FirstOrDefault();
            if (det != null)
            {
                inv = db.Invoices.Where(O => O.Id == det.InvoiceId).Include(x=>x.Invoicedetail).FirstOrDefault();
                return Task.FromResult(inv);
            }
            else
            {
                inv = null;
                return Task.FromResult(inv);
            }

        }

    }
}
