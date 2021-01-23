using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrireksaAppContext.Models
{
    public partial class Invoicedetail
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int PenjualanId { get; set; }

        public virtual Invoices Invoices { get; set; }

        [NotMapped]
        public virtual Penjualan Penjualan { get; set; }
    }
}
