using System;
using System.Collections.Generic;

namespace TrireksaAppContext.Models
{
    public partial class Invoices
    {
        public Invoices()
        {
            Invoicedetail = new HashSet<Invoicedetail>();
        }

        public int Id { get; set; }
        public int Number { get; set; }
        public int CustomerId { get; set; }
        public bool IsDelivery { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string ReciverBy { get; set; }
        public DateTime? ReciveDate { get; set; }
        public DateTime? DeadLine { get; set; }
        public InvoicePayType InvoicePayType { get; set; }
        public int UserId { get; set; }
        public DateTime? PaidDate { get; set; }
        public DateTime? CreateDate { get; set; }
        
        public virtual ICollection<Invoicedetail> Invoicedetail { get; set; }
    }
}
