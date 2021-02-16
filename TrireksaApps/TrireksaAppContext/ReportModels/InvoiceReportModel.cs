using System;
using TrireksaAppContext.Models;

namespace TrireksaAppContext.ReportModels
{
    public class InvoiceReportModel : BaseNotify
    {
        public DateTime CreateDate { get; set; }
        public string CustomerName { get; set; }
        public DateTime DeadLine { get; set; }
        public string NumberView { get; set; }
        public string Terbilang { get; set; }

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

    
}
