using System;
using System.Collections.Generic;

namespace TrireksaAppContext.Models
{
    public partial class Packinglist
    {
        public int Id { get; set; }
        public int ManifestId { get; set; }
        public int PenjualanId { get; set; }
        public int PackNumber { get; set; }
        public int CollyNumber { get; set; }
        public int? CollyId { get; set; }

        public virtual Colly Colly { get; set; }
        public virtual Manifestoutgoing Manifest { get; set; }
    }
}
