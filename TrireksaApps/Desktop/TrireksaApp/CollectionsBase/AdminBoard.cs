using ModelsShared.Models;
using ModelsShared.ReportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaApp.Common;

namespace TrireksaApp.CollectionsBase
{
    public class AdminBoard
    {
        private Client client = new Client("Dashboard");

        internal Task<double> GetPenjualanBulan(DateTime date)
        {
            var uri = $"GetPenjualanBulan/{date.Month}/{date.Year}";
            return client.GetAsync<double>(uri);
        }

        internal Task<List<PenjualanReportModel>> GetPenjualanNotPaid()
        {
            return client.GetAsync<List<PenjualanReportModel>>("GetPenjualanNotPaid");
        }

        internal Task<List<PenjualanReportModel>> GetPenjualanNotStatus()
        {
            return client.GetAsync<List<PenjualanReportModel>>("GetPenjualanNotHaveStatus");
        }

        internal  Task<List<Invoice>> GetInvoiceNotYetPaid()
        {
            return client.GetAsync<List<Invoice>>("GetInvoiceNotYetPaid");
        }

        internal Task<List<Invoice>> GetInvoiceNotYetDelivery()
        {
            return client.GetAsync<List<Invoice>>("GetInvoiceNotYetDelivery");
        }
        internal Task<List<Invoice>> GetInvoiceNotYetRecive()
        {
            return client.GetAsync<List<Invoice>>("GetInvoiceNotYetRecive");
        }

        internal Task<List<Invoice>> GetInvoiceJatuhTempo()
        {
            return client.GetAsync<List<Invoice>>("GetInvoiceJatuhTempo");
        }

        internal Task<List<PenjualanReportModel>> GetPenjualanNotYetSend()
        {
            return client.GetAsync<List<PenjualanReportModel>>("GetPenjualanNotYetSend");
        }
    }
}
