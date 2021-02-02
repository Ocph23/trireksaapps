using System;
using System.Collections.Generic;
using System.Text;
using TrireksaAppContext.ReportModels;

namespace TrireksaAppContext.Models
{
    public class DashboardModel
    {
        public double PenjualanBulanIni { get; set; }
        public double PenjualanBulanLalu { get;  set; }
        public double PenjualanDuaBulanLalu { get;  set; }
        public IEnumerable<PenjualanReportModel> PenjualanNotHaveStatus { get;  set; }
        public IEnumerable<Invoices> InvoiceNotPaid { get;  set; }
        public IEnumerable<PenjualanReportModel> PenjualanNotYetSend { get;  set; }
        public IEnumerable<PenjualanReportModel> PenjualanNotPaid { get;  set; }
        public IEnumerable<Invoices> InvoiceNotYetDelivery { get;  set; }
        public IEnumerable<Invoices> InvoiceJatuhTempo { get;  set; }
        public IEnumerable<Invoices> InvoiceNotYetRecive { get;  set; }
    }
}
