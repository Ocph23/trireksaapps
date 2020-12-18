
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaAppContext
{
    public class Helpers
    {
        internal static int GenerateOutgoingCode()
        {
            using (var db = new ApplicationDbContext())
            {
                var u = db.Manifestoutgoing.LastOrDefault();
                if (u != null)
                {
                    return u.Code + 1;
                }
                else
                {
                    return 1;
                }

            }
        }

        public static string Address
        {
            get
            {
                /* var conf = new MyConfig();
                 var address = conf.GetStringValue("Address");*/
                var address = "Ini Alamat Dari helper";
                return address;
            }
        }


    }
}
