namespace ShareModel
{
    public class DashboardModel
    {
        public double PenjualanBulanIni { get; set; }
        public double PenjualanBulanLalu { get;  set; }
        public double PenjualanDuaBulanLalu { get;  set; }
        public int PenjualanNotHaveStatus { get;  set; }
        public int InvoiceNotPaid { get;  set; }
        public int PenjualanNotYetSend { get;  set; }
        public int PenjualanNotPaid { get;  set; }
        public int InvoiceNotYetDelivery { get;  set; }
        public int InvoiceJatuhTempo { get;  set; }
        public int InvoiceNotYetRecive { get;  set; }
    }
}
