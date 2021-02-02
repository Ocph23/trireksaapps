using ModelsShared.ReportModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsShared.Models
{
    public class DashboardModel
    {
        public double PenjualanBulanIni { get; set; }
        public double PenjualanBulanLalu { get;  set; }
        public double PenjualanDuaBulanLalu { get;  set; }
        public IEnumerable<PenjualanReportModel> PenjualanNotHaveStatus { get;  set; }
        public IEnumerable<Invoice> InvoiceNotPaid { get;  set; }
        public IEnumerable<PenjualanReportModel> PenjualanNotYetSend { get;  set; }
        public IEnumerable<PenjualanReportModel> PenjualanNotPaid { get;  set; }
        public IEnumerable<Invoice> InvoiceNotYetDelivery { get;  set; }
        public IEnumerable<Invoice> InvoiceJatuhTempo { get;  set; }
        public IEnumerable<Invoice> InvoiceNotYetRecive { get;  set; }
    }
}
