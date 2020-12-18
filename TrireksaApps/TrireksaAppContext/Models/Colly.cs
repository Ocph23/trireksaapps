using System;
using System.Collections.Generic;

namespace TrireksaAppContext.Models
{
    public partial class Colly
    {
        public Colly()
        {
            Packinglist = new HashSet<Packinglist>();
        }

        public int Id { get; set; }
        public int PenjualanId { get; set; }
        public int? CollyNumber { get; set; }
        public double Weight { get; set; }
        public double? Long { get; set; }
        public double? Hight { get; set; }
        public double? Wide { get; set; }
        public TypeOfWeight TypeOfWeight { get; set; }
        public bool IsSended { get; set; }
        public double WeightVolume { get; set; }

        public virtual Penjualan Penjualan { get; set; }
        public virtual ICollection<Packinglist> Packinglist { get; set; }
    }
}
