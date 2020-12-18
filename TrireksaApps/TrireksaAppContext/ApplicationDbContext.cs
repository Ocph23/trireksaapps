using Microsoft.EntityFrameworkCore;
using TrireksaAppContext.Models;

namespace TrireksaAppContext
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Cityagentcanaccess> Cityagentcanaccess { get; set; }
        public virtual DbSet<Colly> Colly { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Deliverystatus> Deliverystatus { get; set; }
        public virtual DbSet<Invoicedetail> Invoicedetail { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<Manifestinformation> Manifestinformation { get; set; }
        public virtual DbSet<Manifestoutgoing> Manifestoutgoing { get; set; }
        public virtual DbSet<Packinglist> Packinglist { get; set; }
        public virtual DbSet<Penjualan> Penjualan { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Port> Port { get; set; }
        public virtual DbSet<Price> Price { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Ships> Ships { get; set; }
        public virtual DbSet<Userclaims> Userclaims { get; set; }
        public virtual DbSet<Userlogins> Userlogins { get; set; }
        public virtual DbSet<Userprofile> Userprofile { get; set; }
        public virtual DbSet<Userrole> Userrole { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=;database=trireksapenjualan");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>(entity =>
            {
                entity.ToTable("agent");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Address).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CityId)
                    .HasColumnName("CityID")
                    .HasColumnType("int(5)");

                entity.Property(e => e.ContactName)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Handphone)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''''''");

                entity.Property(e => e.Npwp)
                    .HasColumnName("NPWP")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CityCode)
                    .HasMaxLength(5)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CityName)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''''''");

                entity.Property(e => e.Regency)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasDefaultValueSql("''''''");
            });

            modelBuilder.Entity<Cityagentcanaccess>(entity =>
            {
                entity.ToTable("cityagentcanaccess");

                entity.HasIndex(e => e.AgentId)
                    .HasName("AgentIDOnCity");

                entity.HasIndex(e => new { e.AgentId, e.CityId })
                    .HasName("AgentId")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AgentId).HasColumnType("int(5)");

                entity.Property(e => e.CityId).HasColumnType("int(5)");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Cityagentcanaccess)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AgentIDOnCity");
            });

            modelBuilder.Entity<Colly>(entity =>
            {
                entity.ToTable("colly");

                entity.HasIndex(e => e.PenjualanId)
                    .HasName("STT");

                entity.HasIndex(e => new { e.PenjualanId, e.CollyNumber })
                    .HasName("STTColly")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CollyNumber)
                    .HasColumnType("int(5)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Hight).HasDefaultValueSql("'0'");

                entity.Property(e => e.Long).HasDefaultValueSql("'0'");

                entity.Property(e => e.PenjualanId).HasColumnType("int(11)");

                entity.Property(e => e.Wide).HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Penjualan)
                    .WithMany(p => p.Colly)
                    .HasForeignKey(d => d.PenjualanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_collies_penjualan1");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnType("tinytext")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CityId)
                    .HasColumnName("CityID")
                    .HasColumnType("int(5)");

                entity.Property(e => e.ContactName)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''''''");

                entity.Property(e => e.CustomerType).HasColumnType("tinyint(3)");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Handphone)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Phone1)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Phone2)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Deliverystatus>(entity =>
            {
                entity.ToTable("deliverystatus");

                entity.HasIndex(e => e.PenjualanId)
                    .HasName("STTID");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnType("tinytext")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.PenjualanId).HasColumnType("int(11)");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ReciveDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ReciveName)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''''''");

                entity.HasOne(d => d.Penjualan)
                    .WithMany(p => p.Deliverystatus)
                    .HasForeignKey(d => d.PenjualanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("deliveryStatus");
            });

            modelBuilder.Entity<Invoicedetail>(entity =>
            {
                entity.ToTable("invoicedetail");

                entity.HasIndex(e => e.InvoiceId)
                    .HasName("InvoiceId");

                entity.HasIndex(e => e.PenjualanId)
                    .HasName("invoicedatailSTT");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.InvoiceId).HasColumnType("int(11)");

                entity.Property(e => e.PenjualanId).HasColumnType("int(11)");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.Invoicedetail)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("InvoiceId");

                entity.HasOne(d => d.Penjualan)
                    .WithMany(p => p.Invoicedetail)
                    .HasForeignKey(d => d.PenjualanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_invoicedetail_penjualan1");
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.ToTable("invoices");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CustomerId).HasColumnType("int(11)");

                entity.Property(e => e.DeadLine)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DeliveryDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.InvoicePayType)
                    .IsRequired()
                    .HasColumnType("enum('None','Transfer','Chash')")
                    .HasDefaultValueSql("'''None'''");

                entity.Property(e => e.Number).HasColumnType("int(11)");

                entity.Property(e => e.PaidDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ReciveDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ReciverBy)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''''''");

                entity.Property(e => e.UserId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Manifestinformation>(entity =>
            {
                entity.ToTable("manifestinformation");

                entity.HasIndex(e => e.ManifestId)
                    .HasName("fk_manifestinformation_manifestoutgoing1_idx");

                entity.HasIndex(e => new { e.ManifestId, e.ManifestType })
                    .HasName("ManifestIDAndManifestType")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Address).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ArmadaName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''''''");

                entity.Property(e => e.Contact).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CrewName)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ManifestId).HasColumnType("int(11)");

                entity.Property(e => e.ReferenceNumber)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Manifest)
                    .WithMany(p => p.Manifestinformation)
                    .HasForeignKey(d => d.ManifestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_manifestinformation_manifestoutgoing1");
            });

            modelBuilder.Entity<Manifestoutgoing>(entity =>
            {
                entity.ToTable("manifestoutgoing");

                entity.HasIndex(e => e.AgentId)
                    .HasName("AgentId");

                entity.HasIndex(e => e.Destination)
                    .HasName("DestinationId");

                entity.HasIndex(e => e.Origin)
                    .HasName("OriginId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AgentId).HasColumnType("int(11)");

                entity.Property(e => e.Code).HasColumnType("int(10)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Destination)
                    .HasColumnType("int(5)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.OnDestinationPort)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.OnOriginPort)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Origin)
                    .HasColumnType("int(5)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ReferenceId)
                    .HasColumnName("ReferenceID")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(5)")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Manifestoutgoing)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AgentId");

                entity.HasOne(d => d.DestinationNavigation)
                    .WithMany(p => p.ManifestoutgoingDestinationNavigation)
                    .HasForeignKey(d => d.Destination)
                    .HasConstraintName("DestinationId");

                entity.HasOne(d => d.OriginNavigation)
                    .WithMany(p => p.ManifestoutgoingOriginNavigation)
                    .HasForeignKey(d => d.Origin)
                    .HasConstraintName("OriginId");
            });

            modelBuilder.Entity<Packinglist>(entity =>
            {
                entity.ToTable("packinglist");

                entity.HasIndex(e => e.CollyId)
                    .HasName("CollyId");

                entity.HasIndex(e => e.ManifestId)
                    .HasName("ManifestID");

                entity.HasIndex(e => new { e.PenjualanId, e.CollyNumber })
                    .HasName("stt");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CollyId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CollyNumber).HasColumnType("int(5)");

                entity.Property(e => e.ManifestId)
                    .HasColumnName("ManifestID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PackNumber).HasColumnType("int(5)");

                entity.Property(e => e.PenjualanId).HasColumnType("int(11)");

                entity.HasOne(d => d.Colly)
                    .WithMany(p => p.Packinglist)
                    .HasForeignKey(d => d.CollyId)
                    .HasConstraintName("CollyId");

                entity.HasOne(d => d.Manifest)
                    .WithMany(p => p.Packinglist)
                    .HasForeignKey(d => d.ManifestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ManifestId");
            });

            modelBuilder.Entity<Penjualan>(entity =>
            {
                entity.ToTable("penjualan");

                entity.HasIndex(e => e.FromCity)
                    .HasName("fk_penjualan_city1_idx");

                entity.HasIndex(e => e.ReciverId)
                    .HasName("ReciverID");

                entity.HasIndex(e => e.ShiperId)
                    .HasName("fk_penjualan_customer1_idx");

                entity.HasIndex(e => e.Stt)
                    .HasName("STT_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ToCity)
                    .HasName("fk_penjualan_city2_idx");

                entity.HasIndex(e => new { e.Id, e.ShiperId, e.ReciverId })
                    .HasName("ShiperID");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ChangeDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("tinytext");

                entity.Property(e => e.CustomerIdIsPay).HasColumnType("int(11)");

                entity.Property(e => e.DoNumber).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromCity).HasColumnType("int(11)");

                entity.Property(e => e.Note)
                    .HasColumnType("tinytext")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Price).HasColumnType("double(11,2)");

                entity.Property(e => e.ReciverId)
                    .HasColumnName("ReciverID")
                    .HasColumnType("int(5)");

                entity.Property(e => e.ShiperId)
                    .HasColumnName("ShiperID")
                    .HasColumnType("int(5)");

                entity.Property(e => e.Stt)
                    .HasColumnName("STT")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ToCity).HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(5)")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.FromCityNavigation)
                    .WithMany(p => p.PenjualanFromCityNavigation)
                    .HasForeignKey(d => d.FromCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_penjualan_city1");

                entity.HasOne(d => d.Reciver)
                    .WithMany(p => p.PenjualanReciver)
                    .HasForeignKey(d => d.ReciverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Penjualan_customer2");

                entity.HasOne(d => d.Shiper)
                    .WithMany(p => p.PenjualanShiper)
                    .HasForeignKey(d => d.ShiperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_penjualan_customer1");

                entity.HasOne(d => d.ToCityNavigation)
                    .WithMany(p => p.PenjualanToCityNavigation)
                    .HasForeignKey(d => d.ToCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_penjualan_city2");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("photo");

                entity.HasIndex(e => e.PenjualanId)
                    .HasName("fk_photos_penjualan1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Ext)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.File)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Path)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.PenjualanId).HasColumnType("int(11)");

                entity.HasOne(d => d.Penjualan)
                    .WithMany(p => p.Photo)
                    .HasForeignKey(d => d.PenjualanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_photos_penjualan1");
            });

            modelBuilder.Entity<Port>(entity =>
            {
                entity.ToTable("port");

                entity.HasIndex(e => e.CityId)
                    .HasName("fk_ports_city1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CityId)
                    .HasColumnName("CityID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.PortType).HasDefaultValueSql("'0'");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Port)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ports_city1");
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.ToTable("price");

                entity.HasIndex(e => e.FromCity)
                    .HasName("Price_City_Shiper");

                entity.HasIndex(e => e.ShiperId)
                    .HasName("Price_Customer_shiper");

                entity.HasIndex(e => e.ToCity)
                    .HasName("Price_Customer_Reciver_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.FromCity)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ShiperId).HasColumnType("int(11)");

                entity.Property(e => e.ToCity)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.FromCityNavigation)
                    .WithMany(p => p.PriceFromCityNavigation)
                    .HasForeignKey(d => d.FromCity)
                    .HasConstraintName("Price_City_Shiper");

                entity.HasOne(d => d.Shiper)
                    .WithMany(p => p.Price)
                    .HasForeignKey(d => d.ShiperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Price_Customer_shiper");

                entity.HasOne(d => d.ToCityNavigation)
                    .WithMany(p => p.PriceToCityNavigation)
                    .HasForeignKey(d => d.ToCity)
                    .HasConstraintName("Price_city_Reciver");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Ships>(entity =>
            {
                entity.ToTable("ships");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''''''");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''''''");

                entity.Property(e => e.Route)
                    .HasColumnType("longtext")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Userclaims>(entity =>
            {
                entity.ToTable("userclaims");

                entity.HasIndex(e => e.Id)
                    .HasName("Id")
                    .IsUnique();

                entity.HasIndex(e => e.UserId)
                    .HasName("UserId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ClaimType)
                    .HasColumnType("longtext")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ClaimValue)
                    .HasColumnType("longtext")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userclaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("ApplicationUser_Claims");
            });

            modelBuilder.Entity<Userlogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
                    .HasName("PRIMARY");

                entity.ToTable("userlogins");

                entity.HasIndex(e => e.UserId)
                    .HasName("ApplicationUser_Logins");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userlogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("ApplicationUser_Logins");
            });

            modelBuilder.Entity<Userprofile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("userprofile");

                entity.HasIndex(e => e.UserCode)
                    .HasName("fk_userprofile_users1_idx");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''''''");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''''''");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Photo)
                    .HasColumnType("longblob")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UserCode)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.UserCodeNavigation)
                    .WithMany(p => p.Userprofile)
                    .HasForeignKey(d => d.UserCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_userprofile_users1");
            });

            modelBuilder.Entity<Userrole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PRIMARY");

                entity.ToTable("userrole");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IdentityRole_Users");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Userrole)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("IdentityRole_Users");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userrole)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("ApplicationUser_Roles");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.AccessFailedCount).HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LockoutEndDateUtc)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.PasswordHash)
                    .HasColumnType("longtext")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnType("longtext")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SecurityStamp)
                    .HasColumnType("longtext")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
