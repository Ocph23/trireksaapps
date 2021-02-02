using System;
using System.Collections.Generic;

namespace TrireksaAppContext.Models
{
    public partial class Customer
    {
        public Customer()
        {
           // PenjualanReciver = new HashSet<Penjualan>();
            //PenjualanShiper = new HashSet<Penjualan>();
           // Price = new HashSet<Price>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public CustomerType CustomerType { get; set; }
        public string ContactName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Handphone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int CityId { get; set; }

        public virtual ICollection<Invoices> Invoices{ get; set; }
        public virtual ICollection<Penjualan> PenjualanReciver { get; set; }
        public virtual ICollection<Penjualan> PenjualanShiper { get; set; }
        public virtual ICollection<Price> Price { get; set; }
        public virtual City City { get; set; }
    }
}
