using System;
using System.Collections.Generic;

namespace TrireksaAppContext.Models
{
    public partial class Deliverystatus
    {
        public int Id { get; set; }
        public int PenjualanId { get; set; }
        public DateTime? ReciveDate { get; set; }
        public string ReciveName { get; set; }
        public string Phone { get; set; }
        public bool IsSignIn { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

        public virtual Penjualan Penjualan { get; set; }
    }
}
