using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TrireksaAppContext.Models
{

    [Table("penjualan")]
    public partial class Penjualan
    {
        public Penjualan()
        {
            //Colly = new HashSet<Colly>();
            //Invoicedetail = new HashSet<Invoicedetail>();
            //Photo = new HashSet<Photo>();
        }

        public int Stt { get; set; }
        public int Id { get; set; }
        public int ShiperId { get; set; }
        public int ReciverId { get; set; }
        public double Price { get; set; }
        public TypeOfWeight TypeOfWeight { get; set; }
        public PayType PayType { get; set; }
        public PortType PortType { get; set; }
        public int CustomerIdIsPay { get; set; }
        public double PackingCosts { get; set; }
        public double Tax { get; set; }
        public double Etc { get; set; }
        public int? UserId { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Actived { get; set; }
        public bool IsPaid { get; set; }
        public string Content { get; set; }
        public string DoNumber { get; set; }
        public string Note { get; set; }
        public int FromCity { get; set; }
        public int ToCity { get; set; }

        public double Total
        {
            get
            {

                if (Colly != null && Colly.Count > 0)
                {
                    double berat = 0;
                    berat = Colly.Sum(O => O.Weight);
                    var biaya = (berat * this.Price) + this.PackingCosts + this.Etc;
                    var tax = biaya * (this.Tax / 100);
                    return  biaya + tax;
                }
                return 0;
            }
        }

        public virtual Customer Reciver { get; set; }
        public virtual Customer Shiper { get; set; }
        public virtual City FromCityNavigation { get; set; }
        public virtual City ToCityNavigation { get; set; }
        public virtual Deliverystatus Deliverystatus { get; set; }
        public virtual ICollection<Colly> Colly { get; set; }
        public virtual ICollection<Invoicedetail> Invoicedetail { get; set; }
        public virtual ICollection<Photo> Photo { get; set; }
    }
}