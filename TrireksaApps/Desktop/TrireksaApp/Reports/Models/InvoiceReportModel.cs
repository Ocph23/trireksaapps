using System;
using ModelsShared;
using ModelsShared.Models;

namespace TrireksaApp.Reports.Models
{
    public class InvoiceReportModel : BaseNotify
    {
        public DateTime CreateDate { get; internal set; }
        public string CustomerName { get; internal set; }
        public DateTime DeadLine { get; internal set; }
        public string NumberView { get; internal set; }
        public string Terbilang { get; internal set; }

        public int Id { get; set; }

        public int PenjualanId { get; set; }

        public int InvoiceId { get; set; }

        public string Reciver { get; set; }

        public string Shiper { get; set; }

        public int Pcs { get; set; }

        public double Weight { get; set; }

        public double Price { get; set; }

        public double Total { get; set; }
        public DateTime ChangeDate { get; set; }

        public bool IsSelected { get; set; }
        public double PackingCosts { get; set; }
        public double Etc { get; set; }
        public double Tax { get; set; }
        public int STT { get; set; }
        public string DoNumber { get; set; }
        public string Tujuan { get; set; }
        public PortType PortType { get; set; }
        public virtual string Via
        {
            get
            {
                switch (PortType)
                {
                    case PortType.Sea:
                        return "Laut";
                    case PortType.Air:
                        return "Udara";
                    case PortType.Land:
                        return "Darat";
                    default:
                        return string.Empty;
                }
            }
            set
            {
                SetProperty(ref _via, value);
            }
        }
        private string _via;
    }

    public class InvoiceReport : ModelsShared.Models.Invoice
    {
        public InvoiceReport(Invoice item)
        {
            this.Number = item.Number;
            this.InvoiceStatus = item.InvoiceStatus;
            this.InvoicePayType = item.InvoicePayType;
            this.Biaya = item.Biaya;
            this.CreateDate = item.CreateDate;
            this.CustomerName = item.Customer==null?item.CustomerName:item.Customer.Name;
            this.Tax = item.Tax;
            this.Biaya = item.Biaya;
            Total = item.Total;
            PaidDate = item.PaidDate;
            DeadLine = item.DeadLine;
        }

        public string Status
        {
            get { return this.InvoiceStatus.ToString(); }
        }

        public string PaymentType { get { return InvoicePayType.ToString(); } }
    }
}
