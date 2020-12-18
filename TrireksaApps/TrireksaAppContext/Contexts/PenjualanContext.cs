using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrireksaAppContext.Models;
using TrireksaAppContext.ReportModels;

namespace TrireksaAppContext
{
    public class PenjualanContext
    {
        private readonly ApplicationDbContext db;

        public PenjualanContext(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        public Task<IEnumerable<Penjualan>> Get(DateTime start, DateTime end)
        {
            var results = db.Penjualan.Where(x=>x.ChangeDate.Value >= start && x.ChangeDate.Value <=end)
                .Include(x => x.Colly)
                .Include(x => x.Shiper)
                .Include(x => x.Reciver)
                .Include(x => x.FromCityNavigation)
                .Include(x => x.ToCityNavigation)
                .Include(x => x.Invoicedetail)
                .Include(x => x.Deliverystatus);
            return Task.FromResult(results.AsEnumerable());
        }

        // POST: api/Penjualans
        public Task<int> NewSTT()
        {
            try
            {
                var result = db.Penjualan.Select(O => new { O.Stt }).Last();
                if (result != null)
                    return Task.FromResult(result.Stt + 1);
                else
                    return Task.FromResult(1);
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }


        public async Task<Penjualan> InsertAndGetItem(Penjualan value)
        {
            var date = DateTime.Now;
            value.ChangeDate = date;
            value.UpdateDate = date;
            if (value.Colly != null && value.Colly.Count > 0)
            {
                try
                {
                    if (value.Stt == 0)
                        value.Stt = await NewSTT();

                    db.Penjualan.Add(value);

                    if(await db.SaveChangesAsync()<=0)
                        throw new SystemException(MessageCollection.Message(MessageType.SaveFail));

                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
            else
                throw new SystemException("Item Barang Belum ada");
            return value;

        }

        public async Task<Penjualan> Update(int id, Penjualan value)
        {
            var date = DateTime.Now;
            value.UpdateDate = date;
            if (value.Colly != null && value.Colly.Count > 0)
            {
                try
                {
                    var existsData = db.Penjualan.Where(x=>x.Id==id).Include(x => x.Colly).FirstOrDefault();
                    if (existsData == null)
                        throw new SystemException("Data Not Found !");

                    db.Entry(value).CurrentValues.SetValues(value);
                    if (await db.SaveChangesAsync() <= 0)
                        throw new SystemException("Data Not Saved !");

                    return value;
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
            else
                throw new SystemException("Item Berang Belum ada");

        }

        public async Task<Deliverystatus> UpdateDeliveryStatusBySTT(int id, Deliverystatus obj)
        {
            try
            {
                var res = db.Penjualan.Where(O => O.Stt == id).FirstOrDefault();
                if (res != null)
                {
                    obj.PenjualanId = res.Id;

                    var deliveryId = db.Deliverystatus.Where(O => O.PenjualanId == res.Id).FirstOrDefault().Id;
                    if (deliveryId <= 0)
                        throw new SystemException("Delivery Status Tidak Ditemukan");
                    else
                        obj.Id = deliveryId;

                    db.Entry(obj).CurrentValues.SetValues(obj);

                    if (await db.SaveChangesAsync()>0)
                        return obj;
                    else
                        throw new SystemException("Tidak Tersimpan");
                }
                else
                    throw new SystemException("STT Tidak Ditemukan");

            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        private Task<bool> CheckPortAccess(Penjualan value)
        {
            var ports = db.Port.Where(O => O.CityId == value.ToCity && O.PortType == value.PortType).FirstOrDefault();
            if (ports != null)
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }

        public Task<IEnumerable<Penjualan>> GetByParameter(int agentId, PortType type)
        {
            var results = from c in db.Cityagentcanaccess.Where(O => O.AgentId == agentId)
                         join p in db.Penjualan.Where(O => O.PortType == type)
                         .Include(x=>x.Shiper)
                         .Include(x=>x.Reciver)
                         .Include(x=>x.Colly.Any(m=>m.IsSended)) 
                         on c.CityId equals p.ToCity
                         select p;

            return Task.FromResult(results.AsEnumerable());
        }

        public Task<Penjualan> GetBySTT(int STT)
        {
            var result = db.Penjualan.Where(O => O.Stt == STT).Include(x=>x.Colly).FirstOrDefault();
            return Task.FromResult(result);
        }


        public Task<Penjualan> GetById(int penjualanId)
        {
            var result = db.Penjualan.Where(O => O.Id == penjualanId).Include(x => x.Colly).FirstOrDefault();
            return Task.FromResult(result);
        }


        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task< IEnumerable<Penjualan>> GetPenjualanNotPaid(int Id)
        {
            var result = db.Penjualan.Where(O => O.PayType ==  PayType.Credit && O.CustomerIdIsPay == Id && O.IsPaid == false)
                .Include(x=>x.Colly)
                .Include(x=>x.Shiper)
                .Include(x=>x.Reciver)
                .Include(x=>x.Deliverystatus);
         
            return Task.FromResult(result.AsEnumerable());
        }

        public async Task<Deliverystatus> UpdateDeliveryStatus(Deliverystatus obj)
        {
            var exitsdb = db.Deliverystatus.Where(x => x.Id == obj.Id).FirstOrDefault();
            db.Entry(obj).CurrentValues.SetValues(obj);
            if (await db.SaveChangesAsync() <= 0)
                throw new SystemException("Data Not Saved !");
            return obj;
        }

        public Task<bool> IsSended(int Id)
        {
            var result = db.Colly.Where(O => O.PenjualanId == Id && O.IsSended == true);
            if (result.Count() > 0)
            {
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }



        public Task<List<PenjualanReportModel>> GetPenjualanFromTo(DateTime startDate, DateTime ended)
        {

            throw new NotImplementedException();
            //try
            //{
            //    var sp = string.Format("GetPenjualanFromTo");
            //    var cmd = db.CreateCommand();
            //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //    cmd.CommandText = sp;
            //    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("startDate", startDate));
            //    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("ended", ended));
            //    var dr = cmd.ExecuteReader();

            //    var list = MappingProperties<PenjualanReportModel>.MappingTable(dr);
            //    dr.Close();
            //    foreach (var item in list)
            //    {
            //        item.From = startDate;
            //        item.To = ended;
            //    }
            //    return list;
            //}
            //catch (Exception ex)
            //{

            //    throw new SystemException(ex.Message);
            //}
        }
    }
}

