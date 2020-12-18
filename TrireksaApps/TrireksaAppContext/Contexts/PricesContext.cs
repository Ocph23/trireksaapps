
using System;
using System.Linq;
using System.Threading.Tasks;
using TrireksaAppContext.Models;

namespace TrireksaAppContext
{
    public class PricesContext
    {
        private ApplicationDbContext db;

        public PricesContext(ApplicationDbContext _db)
        {
            db = _db;
        }
        public Task<Price> GetPricesByCustomer(Price price)
        {
            if (price != null)
            {
                var result = db.Price.Where(O => O.ShiperId == price.ShiperId &&
                O.FromCity == price.FromCity && O.ToCity == price.ToCity && O.PortType == price.PortType && O.PayType == price.PayType).FirstOrDefault();
                if (result == null)
                {
                    result = db.Price.Where(O => O.ShiperId == price.ShiperId &&
                    O.FromCity == price.FromCity && O.ToCity == price.ToCity).FirstOrDefault();
                    if (result == null)
                        return Task.FromResult(default(Price));
                    else
                        return Task.FromResult(result);

                }
                return Task.FromResult(result);
            }
            else
            {
                throw new SystemException("Pengirim atau Penerima tidak ditemukan");
            }
        }

        public async Task<Price> SetPrices(Price price)
        {
            var exsitsPrice = db.Price.Where(O => O.ShiperId == price.ShiperId &&
                               O.FromCity == price.FromCity && O.ToCity == price.ToCity &&
                               O.PortType == price.PortType && O.PayType == price.PayType).FirstOrDefault();
            if (exsitsPrice != null)
            {
                db.Entry(exsitsPrice).CurrentValues.SetValues(price);
            }
            else
            {
                db.Price.Add(price);
            }

            if (await db.SaveChangesAsync() <= 0)
                throw new SystemException("Price Not Saved !");
            return price;
        }
    }
}
