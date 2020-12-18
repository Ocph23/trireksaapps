//using Microsoft.Extensions.Configuration;
//
//using Ocph.DAL.Provider.MySql;
//using Ocph.DAL.Repository;
//using System;
//using System.Configuration;

//namespace TrireksaAppContext
//{
//    public class ApplicationDbContext : MySqlDbConnection, IDisposable
//    {
//        public ApplicationDbContext(IConfiguration config)
//        {
//            var conString = config.GetConnectionString("DefaultConnection");
//            this.ConnectionString = conString;
//        }


//        public IRepository<User> Users { get { return new Repository<User>(this); } }
//        public IRepository<UserRole> UserRoles { get { return new Repository<UserRole>(this); } }
//        public IRepository<Role> Roles { get { return new Repository<Role>(this); } }
//        public IRepository<userprofile> UserProfiles { get { return new Repository<userprofile>(this); } }
//        public IRepository<Penjualan> Penjualans { get { return new Repository<Penjualan>(this); } }
//        public IRepository<Colly> Collies { get { return new Repository<Colly>(this); } }
//        public IRepository<Customer> Customers { get { return new Repository<Customer>(this); } }
//        public IRepository<City> Cities { get { return new Repository<City>(this); } }
//        public IRepository<Port> Ports { get { return new Repository<Port>(this); } }
//        public IRepository<Agent> Agents { get { return new Repository<Agent>(this); } }
//        public IRepository<CityAgentCanAccess> CitiesAgentCanAccess { get { return new Repository<CityAgentCanAccess>(this); } }
//        public IRepository<Ship> Ships { get { return new Repository<Ship>(this); } }
//        public IRepository<ManifestOutgoing> Outgoing { get { return new Repository<ManifestOutgoing>(this); } }
//        public IRepository<ManifestInformation> ManifestInformations { get { return new Repository<ManifestInformation>(this); } }
//        public IRepository<Packinglist> PackingLists { get { return new Repository<Packinglist>(this); } }
//        public IRepository<Invoice> Invoices { get { return new Repository<Invoice>(this); } }
//        public IRepository<InvoiceDetail> InvoiceDetails { get { return new Repository<InvoiceDetail>(this); } }
//        public IRepository<DeliveryStatus> DeliveryStatusses { get { return new Repository<DeliveryStatus>(this); } }
//        public IRepository<Price> Priceses { get { return new Repository<Price>(this); } }
//        public IRepository<TracingModel> Tracings { get { return new Repository<TracingModel>(this); } }
//        public IRepository<Photo> Photos { get { return new Repository<Photo>(this); } }


//        public int GenerateOutgoingCode()
//        {
//            var u = this.Outgoing.GetLastItem();
//                if (u != null)
//                {
//                    return u.Code + 1;
//                }
//                else
//                {
//                    return 1;
//                }

//        }

//    }
//}
