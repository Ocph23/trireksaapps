﻿using Microsoft.EntityFrameworkCore;
using ShareModel;
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
            var results = db.Penjualan.Where(x => x.ChangeDate.Value >= start && x.ChangeDate.Value <= end)
                .Include(x=>x.Colly)
                .Include(x => x.Shiper)
                .Include(x => x.Reciver)
                .Include(x => x.ToCityNavigation)
                .Include(x => x.FromCityNavigation)
                .Include(x => x.Invoicedetail)
                .Include(x => x.Deliverystatus).AsNoTracking();
            return Task.FromResult(results.AsEnumerable());
        }

        // POST: api/Penjualans
        public Task<int> NewSTT()
        {
            try
            {
                var result = db.Penjualan.Select(x => x.Stt).AsEnumerable().LastOrDefault();
                if (result > 0)
                    return Task.FromResult(result + 1);
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

                    db.Set<Penjualan>().Add(value);
                    if(value.FromCityNavigation!=null)
                         db.Entry(value.FromCityNavigation).State = EntityState.Unchanged;
                    if(value.ToCityNavigation!=null)
                        db.Entry(value.ToCityNavigation).State = EntityState.Unchanged;
                    db.Entry(value.Shiper).State = EntityState.Unchanged;
                    db.Entry(value.Reciver).State = EntityState.Unchanged;
                    if (await db.SaveChangesAsync() <= 0)
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

            var trans = db.Database.BeginTransaction();
            var date = DateTime.Now;
            value.UpdateDate = date;
            if (value.Colly != null && value.Colly.Count > 0)
            {
                try
                {
                    var existsData = db.Penjualan.Where(x => x.Id == id).Include(x => x.Colly).FirstOrDefault();
                    if (existsData == null)
                        throw new SystemException("Data Not Found !");

                    db.Entry(existsData).CurrentValues.SetValues(value);
                    if (value.FromCityNavigation != null)
                        db.Entry(value.FromCityNavigation).State = EntityState.Unchanged;
                    if (value.ToCityNavigation != null)
                        db.Entry(value.ToCityNavigation).State = EntityState.Unchanged;
                    db.Entry(value.Shiper).State = EntityState.Unchanged;
                    db.Entry(value.Reciver).State = EntityState.Unchanged;


                    foreach (var item in value.Colly)
                    {
                        if (item.Id > 0)
                        {
                            var colie = db.Colly.SingleOrDefault(x => x.Id == item.Id);
                            db.Entry(colie).CurrentValues.SetValues(item);
                        }
                        else
                            db.Colly.Add(item);
                    }


                    //Delete Colly
                    var colies = db.Colly.Where(x => x.PenjualanId == value.Id);
                    foreach (var itemColy in colies)
                    {
                        var colieExists = value.Colly.SingleOrDefault(x => x.Id == itemColy.Id);
                        if (colieExists == null)
                            db.Colly.Remove(itemColy);
                    }

                    if (await db.SaveChangesAsync() <= 0)
                        throw new SystemException("Data Not Saved !");

                    trans.Commit();
                    return value;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
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

                    if (await db.SaveChangesAsync() > 0)
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

        //private Task<bool> CheckPortAccess(Penjualan value)
        //{
        //    var ports = db.Port.Where(O => O.CityId == value.ToCity && O.PortType == value.PortType).FirstOrDefault();
        //    if (ports != null)
        //        return Task.FromResult(true);
        //    else
        //        return Task.FromResult(false);
        //}

        public Task<IEnumerable<Penjualan>> GetByParameter(int agentId, PortType type)
        {
            var results = from c in db.Cityagentcanaccess.Where(O => O.AgentId == agentId)
                          join p in db.Penjualan.Where(O => O.PortType == type) 
                          .Include(x => x.Shiper)
                          .Include(x => x.Reciver)
                          .Include(x => x.Colly)
                          on c.CityId equals p.ToCity
                          select p;

            var result = results.Select(x => new { x, collies = x.Colly.Where(z => !z.IsSended).ToList() }).AsEnumerable().Where(x => x.collies.Count() > 0);
            foreach (var item in result)
            {
                item.x.Colly = item.collies;
            }

            return Task.FromResult(result.Select(x=>x.x).Where(x=>x.Colly.Count>0).AsEnumerable());
        }

        public Task<Penjualan> GetBySTT(int STT)
        {
            var result = db.Penjualan.Where(O => O.Stt == STT)
                .Include(x => x.Colly)
                .Include(x => x.Shiper)
                .Include(x => x.Reciver)
                .Include(x => x.ToCityNavigation)
                .Include(x => x.FromCityNavigation)
                .Include(x => x.Invoicedetail)
                .Include(x => x.Deliverystatus).AsNoTracking()
                .FirstOrDefault();
            return Task.FromResult(result);
        }


        public Task<Penjualan> GetById(int penjualanId)
        {
            var result = db.Penjualan.Where(O => O.Id == penjualanId).Include(x => x.Colly)
                .Include(x => x.Shiper)
                .Include(x => x.Reciver)
                .Include(x => x.ToCityNavigation)
                .Include(x => x.FromCityNavigation)
                .Include(x => x.Invoicedetail)
                .Include(x => x.Deliverystatus).AsNoTracking()
                .FirstOrDefault();
            return Task.FromResult(result);
        }


        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Penjualan>> GetPenjualanNotPaid(int Id)
        {
            var result = db.Penjualan.Where(O => O.PayType == PayType.Credit && O.CustomerIdIsPay == Id && O.IsPaid == false)
                .Include(x => x.Colly).AsNoTracking()
                .Include(x => x.Shiper).AsNoTracking()
                .Include(x => x.ToCityNavigation).AsNoTracking()
                .Include(x => x.FromCityNavigation).AsNoTracking()
                .Include(x => x.Reciver).AsNoTracking()
                .Include(x => x.Deliverystatus).AsNoTracking();

            return Task.FromResult(result.AsEnumerable());
        }

        public async Task<Deliverystatus> UpdateDeliveryStatus(int id,  Deliverystatus obj)
        {
            var exitsdb = db.Deliverystatus.Where(x => x.Id == id).FirstOrDefault();
            db.Entry(exitsdb).CurrentValues.SetValues(obj);
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



        public Task<IEnumerable<PenjualanReportModel>> GetPenjualanFromTo(DateTime startDate, DateTime ended)
        {

            var results = db.Penjualan.Where(x => x.ChangeDate.Value >= startDate && x.ChangeDate.Value <= ended)
                .Include(x => x.Colly)
                .Include(x => x.Shiper)
                .Include(x => x.Reciver)
                .Include(x => x.ToCityNavigation)
                .Include(x => x.FromCityNavigation)
                .Include(x => x.Invoicedetail)
                .Include(x => x.Deliverystatus).AsNoTracking();

            var data = from a in results
                       select new PenjualanReportModel
                       {
                          Biaya = a.Total,
                           ChangeDate=a.ChangeDate.Value,
                            DoNumber=a.DoNumber, 
                             From=startDate,
                              To=ended,
                               Id=a.Id,
                                PayStatus=  a.IsPaid?"Pay Off":"Not Paid" ,
                                 PayType=a.PayType.ToString(), Pcs=a.Colly.Count.ToString(),
                                  STT=a.Stt.ToString("D6"), 
                           ShiperCity=a.FromCityNavigation.CityName,
                           ReciverCity= a.ToCityNavigation.CityName,
                           Shiper= a.Shiper.Name,
                           Reciver= a.Reciver.Name,
                            Price=a.Price,
                        PortType=a.PortType.ToString()   , Weight= a.Colly.Sum(x=>x.Weight)
                       };

            return Task.FromResult(data.AsEnumerable());
        }
    }
}

