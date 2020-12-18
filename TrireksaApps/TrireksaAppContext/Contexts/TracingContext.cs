using System;
using System.Collections.Generic;
using System.Linq;
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
        public TracingModel GetPenjualan(int STT)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var cmd = db.CreateCommand();
            //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //    cmd.CommandText = "GetPenjualan";
            //    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("STT", STT));
            //    var reader = cmd.ExecuteReader();
            //    var mapp = new MappingColumn(new EntityInfo(typeof(TracingModel)));
            //    var result = mapp.MappingWithoutInclud<TracingModel>(reader);

            //    reader.Close();

            //    var tracing = result.FirstOrDefault();
            //    if (result != null)
            //    {
            //        var packlists = db.PackingLists.Where(O => O.PenjualanId == tracing.Id).GroupBy(O => O.ManifestID).ToList();
            //        tracing.Manifests = new List<ManifestOutgoing>();
            //        foreach (var item in packlists)
            //        {
            //            var manifest = db.Outgoing.Where(O => O.Id == item.Key).FirstOrDefault();
            //            if (manifest != null)
            //                tracing.Manifests.Add(manifest);
            //        }
            //        return tracing;
            //    }
            //    else
            //    {
            //        throw new SystemException(MessageCollection.Message(MessageType.NotFound));
            //    }

            //}
            //catch (Exception ex)
            //{

            //    throw new SystemException(ex.Message);
            //}

        }
    }
}
