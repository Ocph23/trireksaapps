using System;
using System.Collections.Generic;

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
        public virtual byte[] Thumb { get; set; }
        public virtual byte[] Picture { get; internal set; }
        public virtual int Stt { get; internal set; }
    }
}
