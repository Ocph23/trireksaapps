using System;
using System.Collections.Generic;

namespace TrireksaAppContext.Models
{
    public partial class City
    {
        public City()
        {
            PenjualanFromCityNavigation = new HashSet<Penjualan>();
            PenjualanToCityNavigation = new HashSet<Penjualan>();
            Port = new HashSet<Port>();
            PriceFromCityNavigation = new HashSet<Price>();
            PriceToCityNavigation = new HashSet<Price>();
        }

        public int Id { get; set; }
        public string Province { get; set; }
        public string Regency { get; set; }
        public string CityName { get; set; }
        public string CityCode { get; set; }

        public virtual ICollection<Penjualan> PenjualanFromCityNavigation { get; set; }
        public virtual ICollection<Penjualan> PenjualanToCityNavigation { get; set; }
        public virtual ICollection<Port> Port { get; set; }
        public virtual ICollection<Price> PriceFromCityNavigation { get; set; }
        public virtual ICollection<Price> PriceToCityNavigation { get; set; }
    }
}
