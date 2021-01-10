using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrireksaAppContext.Models;

namespace TrireksaAppContext
{
    public class TracingContext
    {
        private ApplicationDbContext db;

        public TracingContext(ApplicationDbContext _db)
        {
            db = _db;
        }
        public Task<TracingModel> GetPenjualan(int STT)
        {
            try
            {
                var data = db.Penjualan.Where(x => x.Stt == STT)
                    .Include(x => x.Colly)
                    .Include(x => x.Reciver)
                    .Include(x => x.Shiper)
                    .Include(x => x.Invoicedetail)
                    .Include(x=>x.Deliverystatus)
                    .Include(x => x.FromCityNavigation)
                    .Include(x => x.ToCityNavigation);


                var model = (from a in data
                             select new TracingModel
                             {
                                 Actived = a.Actived,
                                 ChangeDate = a.ChangeDate,
                                 Content = a.Content,
                                 CustomerIdIsPay = a.CustomerIdIsPay,
                                 DoNumber = a.DoNumber,
                                 Etc = a.Etc,
                                 IsPaid = a.IsPaid,
                                 Note = a.Note,
                                 PackingCosts = a.PackingCosts,
                                 Pcs = a.Colly.Count,
                                 PayType = a.PayType,
                                 Id = a.Id,
                                 ShiperName = a.Shiper.Name,
                                 PortDestinationName = a.ToCityNavigation.CityName,
                                 PortDestionationCode = a.ToCityNavigation.CityCode,
                                 PortOriginCode = a.FromCityNavigation.CityCode,
                                 PortOriginName = a.FromCityNavigation.CityName,
                                 Price = a.Price,
                                 ReciverName = a.Reciver.Name,
                                 STT = a.Stt,
                                 Tax = a.Tax,
                                 Total = a.Total,      
                                 ReciveDate=a.Deliverystatus.ReciveDate,
                                 ReciveName = a.Deliverystatus.ReciveName,
                                 RecievePhone = a.Deliverystatus.Phone,
                                 ReciveCondition = a.Deliverystatus.Description, 
                                 PortType=a.PortType,   
                                 TypeOfWeight = a.TypeOfWeight,
                                 Weight = a.Colly.Sum(x => x.Weight),
                                 UpdateDate = a.UpdateDate.Value,
                             }).FirstOrDefault();

               
                if (model != null)
                {
                    var packlists = db.Packinglist.Where(O => O.PenjualanId == model.Id).AsEnumerable().GroupBy(O => O.ManifestId).ToList();
                    model.Manifests = new List<Manifestoutgoing>();
                    foreach (var item in packlists)
                    {
                        var manifest = db.Manifestoutgoing.Where(O => O.Id == item.Key)
                            .Include(x=>x.DestinationNavigation)
                            .Include(x=>x.OriginNavigation)
                            .Include(x=>x.Information).FirstOrDefault();
                        if (manifest != null)
                            model.Manifests.Add(manifest);
                    }
                    return Task.FromResult(model);
                }
                else
                {
                    throw new SystemException(MessageCollection.Message(MessageType.NotFound));
                }

            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }

        }
    }
}
