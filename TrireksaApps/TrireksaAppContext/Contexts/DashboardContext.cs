using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrireksaAppContext
{
    public class DashboardContext
    {

        //private ApplicationDbContext db;

        //public DashboardContext(ApplicationDbContext dbContext)
        //{
        //    db = dbContext;
        //}
        //public Task<List<Invoice>> GetInvoiceNotYetPaid()
        //{

        //    var sp = string.Format("InvoiceNotYetPaid");
        //    var cmd = db.CreateCommand();
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd.CommandText = sp;
        //    var dr = cmd.ExecuteReader();

        //    var ent = new EntityInfo(typeof(ModelsShared.Models.Invoice));
        //    var list = new MappingColumn(ent).MappingWithoutInclud<Invoice>(dr);
        //    dr.Close();

        //    return Task.FromResult(list);

        //}
        //public Task<double> GetPenjualanBulan(int month, int year)
        //{

        //    var result = db.Penjualans.Where(O => O.ChangeDate.Month == month && O.ChangeDate.Year == year);
        //    foreach (var item in result)
        //    {
        //        item.Details = db.Collies.Where(O => O.PenjualanId == item.Id).ToList();
        //        item.Shiper = db.Customers.Where(O => O.Id == item.ShiperID).FirstOrDefault();
        //        item.Reciver = db.Customers.Where(O => O.Id == item.ReciverID).FirstOrDefault();
        //        item.DeliveryStatus = db.DeliveryStatusses.Where(O => O.PenjualanId == item.Id).FirstOrDefault();
        //    }

        //    return Task.FromResult(result.Sum(O => O.Total));
        //}


        //private Task<List<PenjualanReportModel>> GetPenjualan(string sp)
        //{
        //    try
        //    {
        //        var cmd = db.CreateCommand();
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandText = sp;
        //        var dr = cmd.ExecuteReader();
        //        var list = MappingProperties<PenjualanReportModel>.MappingTable(dr);
        //        dr.Close();

        //        return Task.FromResult(list);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new SystemException(ex.Message);
        //    }

        //}

        //public Task<List<PenjualanReportModel>> GetPenjualanNotHaveStatus()
        //{

        //    try
        //    {
        //        return GetPenjualan("PenjualanNotHaveDeliveryStatus");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new SystemException(ex.Message);
        //    }
        //}


        //public Task<List<PenjualanReportModel>> GetPenjualanNotYetSend()
        //{
        //    try
        //    {
        //        return GetPenjualan("PenjualanNotYetSend");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new SystemException(ex.Message);
        //    }

        //}


        //public Task<List<PenjualanReportModel>> GetPenjualanNotPaid()
        //{
        //    try
        //    {
        //        return GetPenjualan("PenjualanNotPaid");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new SystemException(ex.Message);
        //    }

        //}

        //public Task<List<Invoice>> GetInvoiceNotYetDelivery()
        //{

        //    var sp = string.Format("InvoiceNotYetDelivery");
        //    var cmd = db.CreateCommand();
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd.CommandText = sp;
        //    var dr = cmd.ExecuteReader();

        //    var ent = new EntityInfo(typeof(ModelsShared.Models.Invoice));
        //    var list = new MappingColumn(ent).MappingWithoutInclud<Invoice>(dr);
        //    dr.Close();

        //    return Task.FromResult(list);
        //}

        //public Task<List<Invoice>> GetInvoiceJatuhTempo()
        //{

        //    var sp = string.Format("InvoiceJatuhTempo");
        //    var cmd = db.CreateCommand();
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd.CommandText = sp;
        //    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("Tanggal", DateTime.Now));
        //    var dr = cmd.ExecuteReader();

        //    var ent = new EntityInfo(typeof(ModelsShared.Models.Invoice));
        //    var list = new MappingColumn(ent).MappingWithoutInclud<Invoice>(dr);
        //    dr.Close();

        //    return Task.FromResult(list);

        //}

        //public Task<List<Invoice>> GetInvoiceNotYetRecive()
        //{

        //    var sp = string.Format("InvoiceNotYetRecive");
        //    var cmd = db.CreateCommand();
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd.CommandText = sp;
        //    var dr = cmd.ExecuteReader();

        //    var ent = new EntityInfo(typeof(ModelsShared.Models.Invoice));
        //    var list = new MappingColumn(ent).MappingWithoutInclud<Invoice>(dr);
        //    dr.Close();

        //    return Task.FromResult(list);
        //}


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
    }
}
