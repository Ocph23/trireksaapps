using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrireksaAppContext.Models
{
    public partial class Photo
    {
        public int Id { get; set; }
        public string File { get; set; }
        public string Ext { get; set; }
        public string Path { get; set; }
        public int PenjualanId { get; set; }
        public virtual Penjualan Penjualan { get; set; }
        [NotMapped]
        public virtual byte[] Thumb { get; set; }
        [NotMapped]
        public virtual byte[] Picture { get; set; }
        
    }
}
