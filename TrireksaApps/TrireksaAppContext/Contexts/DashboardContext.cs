using Microsoft.EntityFrameworkCore;
using ShareModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrireksaAppContext.Models;

namespace TrireksaAppContext
{
    public class DashboardContext
    {

        private ApplicationDbContext db;

        public DashboardContext(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
       

        public Task<double> GetPenjualanBulan(int month, int year)
        {

            double total = 0;
            var result = db.Penjualan.Where(O => O.ChangeDate.Value.Month == month && O.ChangeDate.Value.Year == year)
                .Include(x=>x.Colly).AsEnumerable();

            if(result!=null)
                total = result.Sum(x => x.Total);

            return Task.FromResult(total);
        }

        public Task<IEnumerable<PenjualanReportModel>> GetPenjualanNotHaveStatus()
        {
            try
            {
                var result = db.Penjualan
                    .Include(x=>x.Colly)
                     .Include(x => x.Shiper)
                     .Include(x => x.Reciver)
                     .Include(x => x.FromCityNavigation)
                     .Include(x => x.ToCityNavigation)
                     .Include(x => x.Deliverystatus).Where(x => x.Deliverystatus == null || x.Deliverystatus.ReciveDate==null)
                     .Select(p => new PenjualanReportModel() {
                         STT = p.Stt.ToString("D5"),
                         ChangeDate = p.ChangeDate.Value,
                         DoNumber = p.DoNumber,
                         Pcs = p.Colly.Count.ToString(),
                         Reciver = p.Reciver.Name,
                         Shiper = p.Shiper.Name,
                         ReciverCity = p.ToCityNavigation.CityName,
                         ShiperCity = p.FromCityNavigation.CityName , 
                         PayType= p.PayType.ToString(),
                         Weight= p.Colly.Sum(x=>x.Weight)
                     });
                return Task.FromResult(result.AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }


        public Task<IEnumerable<PenjualanReportModel>> GetPenjualanNotYetSend()
        {

            try
            {
                var result = from p in db.Penjualan.Where(x => x.Colly.Any(x => !x.IsSended))
                     .Include(x => x.Colly)
                     .Include(x => x.Shiper)
                     .Include(x => x.Reciver)
                     .Include(x => x.FromCityNavigation)
                     .Include(x => x.ToCityNavigation)
                        select new PenjualanReportModel
                    {
                        
                        STT = p.Stt.ToString("D5"),
                        ChangeDate = p.ChangeDate.Value,
                        DoNumber = p.DoNumber,    Pcs=p.Colly.Count.ToString(),
                          Reciver= p.Reciver.Name, Shiper= p.Shiper.Name, 
                        ReciverCity = p.ToCityNavigation.CityName,
                        ShiperCity = p.FromCityNavigation.CityName

                    };
                return Task.FromResult(result.AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }

        }


        public Task<IEnumerable<PenjualanReportModel>> GetPenjualanNotPaid()
        {
            try
            {
                var result = from p in db.Penjualan.Where(x => !x.IsPaid)
                     .Include(x => x.Colly)
                     .Include(x => x.Shiper)
                     .Include(x => x.Reciver)
                     .Include(x => x.FromCityNavigation)
                     .Include(x => x.ToCityNavigation)
                             select new PenjualanReportModel
                             {

                                 STT = p.Stt.ToString("D5"),
                                 ChangeDate = p.ChangeDate.Value,
                                 DoNumber = p.DoNumber,
                                 Pcs = p.Colly.Count.ToString(),
                                 Reciver = p.Reciver.Name,
                                 Shiper = p.Shiper.Name,
                                 ReciverCity = p.ToCityNavigation.CityName,
                                 ShiperCity = p.FromCityNavigation.CityName

                             };
                return Task.FromResult(result.AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }


        public Task<IEnumerable<Invoices>> GetInvoiceNotYetPaid()
        {

            var result = db.Invoices.Where(x => x.PaidDate == null)
                .Include(x => x.Invoicedetail).ThenInclude(x => x.Penjualan).ThenInclude(x => x.Colly)
                .Include(x => x.Customer).AsEnumerable();
            return Task.FromResult(result);
        }

        public Task<IEnumerable<Invoices>> GetInvoiceNotYetDelivery()
        {

            var invoices = db.Invoices.Where(x => !x.IsDelivery)
                .Include(x=>x.Invoicedetail).ThenInclude(x=>x.Penjualan).ThenInclude(x=>x.Colly)
                .Include(x=>x.Customer);
            return Task.FromResult(invoices.AsEnumerable());

        }

        public Task<IEnumerable<Invoices>> GetInvoiceJatuhTempo()
        {

            var invoices = db.Invoices.Where(x =>x.InvoicePayType== InvoicePayType.None && x.DeadLine <= DateTime.Now)
                .Include(x => x.Invoicedetail).ThenInclude(x => x.Penjualan).ThenInclude(x => x.Colly)
                .Include(x => x.Customer)
                ;
            return Task.FromResult(invoices.AsEnumerable());

        }

        public Task<IEnumerable<Invoices>> GetInvoiceNotYetRecive()
        {

            var invoices = db.Invoices.Where(x => x.ReciveDate == null || string.IsNullOrEmpty(x.ReciverBy))
                .Include(x => x.Invoicedetail).ThenInclude(x => x.Penjualan).ThenInclude(x => x.Colly)
                .Include(x => x.Customer)
                ;
            return Task.FromResult(invoices.AsEnumerable());
        }


        //public async Task<List<PenjualanOfYear>> GetPenjualanThreeYear()
        //{
        //    var list = new List<ModelsShared.Models.PenjualanOfYear>();
        //    var thisyear = DateTime.Now.Year;
        //    var sp = string.Format("PenjualanOfaYear");
        //    var cmd = db.CreateCommand();
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd.CommandText = sp;
        //    for (int i = thisyear; i > thisyear - 3; i--)
        //    {
        //        cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("tahun", i));
        //        var dr = cmd.ExecuteReader();

        //        var ent = new EntityInfo(typeof(ModelsShared.Models.PenjualanOfYear));
        //        list = new MappingColumn(ent).MappingWithoutInclud<PenjualanOfYear>(dr);
        //        if (list.Count > 0)
        //        {
        //            var item = list.FirstOrDefault();
        //            item.Months = await GetPenjualanOfMonth(item.Tahun);
        //            list.Add(item);
        //        }


        //        cmd.Parameters.RemoveAt("tahun");
        //        dr.Close();
        //    }

        //    return list;
        //}

        //private Task<List<PenjualanOfMonth>> GetPenjualanOfMonth(int tahun)
        //{
        //    var list = new List<ModelsShared.Models.PenjualanOfMonth>();
        //    var sp = string.Format("PenjualanOfamount");
        //    var cmd = db.CreateCommand();
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd.CommandText = sp;
        //    for (int i = 1; i <= 12; i++)
        //    {
        //        var date = new DateTime(tahun, i, 1);
        //        cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("Tanggal", date));
        //        var dr = cmd.ExecuteReader();
        //        var ent = new EntityInfo(typeof(ModelsShared.Models.PenjualanOfMonth));
        //        var result = new MappingColumn(ent).MappingWithoutInclud<PenjualanOfMonth>(dr);
        //        if (result.Count > 0)
        //        {
        //            var item = result.FirstOrDefault();
        //            list.Add(item);
        //        }


        //        cmd.Parameters.RemoveAt("Tanggal");
        //        dr.Close();
        //    }

        //    return Task.FromResult(list);
        //}



        public async Task<DashboardModel> GetDashboard()
        {
            try
            {
                var model = new DashboardModel();
                var date = DateTime.Now;
                var blnLalu = date.AddMonths(-1);
                var duaBulanLalu = date.AddMonths(-2);
                var result = db.Penjualan.Where(O => O.ChangeDate.Value >= duaBulanLalu && O.ChangeDate.Value <= date)
                    .Include(x => x.Colly).AsEnumerable();

                if (result != null)
                {
                    model.PenjualanBulanIni = result.Where(x => x.ChangeDate.Value.Month == date.Month && x.ChangeDate.Value.Year == date.Year).Sum(x => x.Total);
                    model.PenjualanBulanLalu = result.Where(x => x.ChangeDate.Value.Month == blnLalu.Month && x.ChangeDate.Value.Year == blnLalu.Year).Sum(x => x.Total);
                    model.PenjualanDuaBulanLalu = result.Where(x => x.ChangeDate.Value.Month == duaBulanLalu.Month && x.ChangeDate.Value.Year == duaBulanLalu.Year).Sum(x => x.Total);
                }
                //model.PenjualanDuaBulanLalu = await GetPenjualanBulan(duaBulanLalu.Month, duaBulanLalu.Year);


                var invoices = db.Invoices.Where(x => x.PaidDate == null || !x.IsDelivery || x.PaidDate==null || x.ReciveDate == null || string.IsNullOrEmpty(x.ReciverBy))
               .Include(x => x.Invoicedetail).ThenInclude(x => x.Penjualan).ThenInclude(x => x.Colly)
               .Include(x => x.Customer).AsEnumerable();

                if (invoices != null)
                {
                    model.InvoiceNotPaid = invoices.Where(x => x.PaidDate == null).Count();
                    model.InvoiceNotYetDelivery = invoices.Where(x => !x.IsDelivery).Count();
                    model.InvoiceNotYetRecive = invoices.Where(x => x.ReciveDate == null || string.IsNullOrEmpty(x.ReciverBy)).Count();
                    model.InvoiceJatuhTempo = (await GetInvoiceJatuhTempo()).Count();
                }

                model.PenjualanNotHaveStatus = (await GetPenjualanNotHaveStatus()).Count();
                model.PenjualanNotYetSend = (await GetPenjualanNotYetSend()).Count();
                model.PenjualanNotPaid = (await GetPenjualanNotPaid()).Count();
                return model;
            }
            catch 
            {
                return new DashboardModel();
            }
        }
    }

    
}
