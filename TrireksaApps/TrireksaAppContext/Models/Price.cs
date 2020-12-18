using System;
using System.Collections.Generic;

namespace TrireksaAppContext.Models
{
    public partial class Price
    {
        public int Id { get; set; }
        public int ShiperId { get; set; }
        public bool PortType { get; set; }
        public bool PayType { get; set; }
        public int? FromCity { get; set; }
        public int? ToCity { get; set; }
        public double PriceValue { get; set; }

        public virtual City FromCityNavigation { get; set; }
        public virtual Customer Shiper { get; set; }
        public virtual City ToCityNavigation { get; set; }
    }
}
