using ModelsShared.Models;
using ModelsShared.ReportModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaMobile.Services
{
    public interface IDashboardService
    {
        public Task<double> GetPenjualanBulan(DateTime date);
        public Task<List<PenjualanReportModel>> GetPenjualanNotPaid();
        public Task<List<PenjualanReportModel>> GetPenjualanNotStatus();
        public Task<List<PenjualanReportModel>> GetPenjualanNotYetSend();
        public Task<List<Invoice>> GetInvoiceNotYetPaid();
        public Task<List<Invoice>> GetInvoiceNotYetDelivery();
        public Task<List<Invoice>> GetInvoiceNotYetRecive();
        public Task<List<Invoice>> GetInvoiceJatuhTempo();
    }

    public class DashboardService: IDashboardService
    {
        private string controller = "api/dashboard";
        public async Task<double> GetPenjualanBulan(DateTime date)
        {
            using (var client= new RestService())
            {
               var uri = $"{controller}/GetPenjualanBulan/{date.Month}/{date.Year}";
               var response = await client.GetAsync(uri);
               if (response.IsSuccessStatusCode)
               {
                    return await response.GetResult<double>();
               }
                return 0;
            }
        }

        public async Task<List<PenjualanReportModel>> GetPenjualanNotPaid()
        {
            using (var client = new RestService())
            {
                var uri = $"{controller}/GetPenjualanNotPaid";
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.GetResult<List<PenjualanReportModel>>();
                }
                return null;
            }
        }

        public async Task<List<PenjualanReportModel>> GetPenjualanNotStatus()
        {

            using (var client = new RestService())
            {
                var uri = $"{controller}/GetPenjualanNotHaveStatus";
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.GetResult<List<PenjualanReportModel>>();
                }
                return null;
            }
        }
        public async Task<List<PenjualanReportModel>> GetPenjualanNotYetSend()
        {
            using (var client = new RestService())
            {
                var uri = $"{controller}/GetPenjualanNotYetSend";
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.GetResult<List<PenjualanReportModel>>();
                }
                return null;
            }
        }

        public async Task<List<Invoice>> GetInvoiceNotYetPaid()
        {
            using (var client = new RestService())
            {
                var uri = $"{controller}/GetInvoiceNotYetPaid";
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.GetResult<List<Invoice>>();
                }
                return null;
            }
        }

        public async Task<List<Invoice>> GetInvoiceNotYetDelivery()
        {
            using (var client = new RestService())
            {
                var uri = $"{controller}/GetInvoiceNotYetDelivery";
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.GetResult<List<Invoice>>();
                }
                return null;
            }
        }
        public async Task<List<Invoice>> GetInvoiceNotYetRecive()
        {
            using (var client = new RestService())
            {
                var uri = $"{controller}/GetInvoiceNotYetRecive";
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.GetResult<List<Invoice>>();
                }
                return null;
            }
        }

        public async Task<List<Invoice>> GetInvoiceJatuhTempo()
        {
            using (var client = new RestService())
            {
                var uri = $"{controller}/GetInvoiceJatuhTempo";
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.GetResult<List<Invoice>>();
                }
                return null;
            }
        }

     
    }
}
