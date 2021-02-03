using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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


        public Task<IEnumerable<Invoices>> Get(DateTime startDate, DateTime endDate)
        {
            var results = db.Invoices.Where(x=>x.CreateDate>=startDate && x.CreateDate<= endDate)
                .Include(x => x.Customer)
                .Include(x => x.Invoicedetail)
                .ThenInclude(x => x.Penjualan).ThenInclude(x => x.Colly)

                ;
            var datas = results.ToList();

            return Task.FromResult(results.ToList().AsEnumerable());
        }


        public Task<Invoices> Get(int Id)
        {
            try
            {
                var inv = db.Invoices.Where(O => O.Id == Id)
                     .Include(x => x.Customer)
                .Include(x => x.Invoicedetail)
                .ThenInclude(x => x.Penjualan).ThenInclude(x => x.Colly)
                .ThenInclude(x => x.Penjualan).ThenInclude(x => x.ToCityNavigation)
                .Include(x => x.Invoicedetail)
                .ThenInclude(x => x.Penjualan).ThenInclude(x => x.FromCityNavigation)
                .Include(x => x.Invoicedetail)
                .ThenInclude(x => x.Penjualan).ThenInclude(x => x.Reciver)
                .Include(x => x.Invoicedetail)
                .ThenInclude(x => x.Penjualan).ThenInclude(x => x.Shiper)
                    .FirstOrDefault();
                return Task.FromResult(inv);

            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<Invoices> Put(int id, Invoices model)
        {
            try
            {
                var existInvoice = db.Invoices.Include(x => x.Customer).Where(x => x.Id == id)
                    .Include(x => x.Invoicedetail).FirstOrDefault();
                if (existInvoice != null)
                {

                    db.Entry(existInvoice.Customer).State = EntityState.Detached;
                    existInvoice.DeadLine = model.DeadLine;

                    foreach (var item in model.Invoicedetail)
                    {
                        if (item.Id <= 0)
                        {
                            db.Entry(item.Penjualan).State = EntityState.Unchanged;
                            item.InvoiceId = existInvoice.Id;
                            db.Invoicedetail.Add(item);
                        }
                    }

                    foreach (var item in existInvoice.Invoicedetail)
                    {
                        var itemExists = model.Invoicedetail.SingleOrDefault(x => x.Id == item.Id);
                        if (itemExists == null)
                        {
                            db.Invoicedetail.Remove(item);
                        }
                    }

                    if (await db.SaveChangesAsync() <= 0)
                        throw new SystemException("Data Not Saved");

                    return existInvoice;
                }
                else
                    throw new SystemException("Not Found !");
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


                if (t.Invoicedetail == null || t.Invoicedetail.Count <= 0)
                    throw new SystemException("Lengkapi Data STT !");

                db.Entry(t.Customer).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                if (!db.Invoices.Any())
                    t.Number++;
                else
                {
                    var lastINvoice = db.Invoices.Max(x => x.Number);
                    t.Number = lastINvoice + 1;
                }

                foreach (var item in t.Invoicedetail)
                {
                    db.Entry(item.Penjualan).State = EntityState.Unchanged;
                }
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
            try
            {
                if (t.DeliveryDate != null)
                    t.IsDelivery = true;

                if (t.ReciveDate != null && string.IsNullOrEmpty(t.ReciverBy))
                    throw new SystemException("Reciver Name Can't Empty");
                var existsData = db.Invoices.Where(x => x.Id == Id).FirstOrDefault();
                if (existsData == null)
                    throw new SystemException("Data Not Found !");

                db.Entry(existsData).CurrentValues.SetValues(t);

                if (await db.SaveChangesAsync() <= 0)
                    throw new SystemException("Data Not Saved");
                return t;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }


        public async Task<Invoices> UpdateInvoiceStatusAction(int Id, Invoices t)
        {
            try
            {
                var existsData = await Get(Id);
                if (existsData == null)
                    throw new SystemException("Data Not Found !");
                db.Entry(existsData).CurrentValues.SetValues(t);
                if (await db.SaveChangesAsync() <= 0)
                    throw new SystemException("Data Not Saved");

                return existsData;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
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
