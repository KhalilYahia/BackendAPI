
using YaznGhanem.Data.SeedData;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Model.Configuration;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Inerfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using YaznGhanem.Data;
using YaznGhanem.Data.Configuration;

namespace YaznGhanem.Model
{
    public class ApplicationDbContext:IdentityDbContext<CustomUser,CustomRole,string>// DbContext /*IdentityDbContext*/
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #region Users Identity tables

        //public DbSet<User> Users { get; set; }
        //public DbSet<UserToken> UserTokens { get; set; }
        //public DbSet<UserClaim> UserClaims { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<RoleClaim> RoleClaims { get; set; }
        //public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<CustomRole> CustomRoles { set; get; }
        public DbSet<CustomUser> CustomUsers { set; get; }

        #endregion

       

        public DbSet<Category> Categories { set; get; }
       
        public DbSet<Language> languages { set; get; }

        public DbSet<FinancialEntitlement> FinancialEntitlements { get; set; }
        public DbSet<FinancialPayment> FinancialPayments { get; set; }
        public DbSet<Domain.Entities.Repository> Repositories { get; set; }
        public DbSet<Repository_InOut> RepositoryInOuts { get; set; }
        public DbSet<RepositoryMaterials> RepositoryMaterials { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<TotalFunds> TotalFunds { get; set; }

        public DbSet<Daily> Dailies { get; set; }
        public DbSet<Cars> Cars { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<DailyChekEmployees> DailyChekEmployees { get; set; }
        public DbSet<Buyers> Buyers { get; set; }
        public DbSet<Refrigerator> Refrigerators { get; set; }
        public DbSet<RefrigeratorDetails> RefrigeratorDetails { get; set; }
        public DbSet<ExternalEnvoices> ExternalEnvoices { get; set; }
        public DbSet<CoolingRooms> CoolingRooms { get; set; }
        public DbSet<OtherSales> OtherSales { get; set; }
        public DbSet<SupplierOfFarms> SupplierOfFarms { get; set; }


        public DbSet<BoFUser> BoFUsers { get; set; }
        public DbSet<BoFTotal> BoFTotal { get; set; }
        public DbSet<BoFOperations> BoFOperations { get; set; }
        public DbSet<BoFOpDetails> BoFOpDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            MyDbContextSeed.SeedData(modelBuilder); // uncomment this to insert data
            //modelBuilder.ApplyConfiguration(new PostConfiguration());

           
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new FinancialEntitlementConfiguration());
            modelBuilder.ApplyConfiguration(new FinancialPaymentConfiguration());
            modelBuilder.ApplyConfiguration(new RepositoryConfiguration());
            modelBuilder.ApplyConfiguration(new RepositoryInOutConfiguration());
            modelBuilder.ApplyConfiguration(new RepositoryMaterialsConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new TotalFundsConfiguration());
           
            modelBuilder.ApplyConfiguration(new FuelConfiguration());
            modelBuilder.ApplyConfiguration(new CarsConfiguration());
            modelBuilder.ApplyConfiguration(new DailyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeesConfiguration());
            modelBuilder.ApplyConfiguration(new DailyChekEmployeesConfiguration());

            modelBuilder.ApplyConfiguration(new BuyersConfiguration());
            modelBuilder.ApplyConfiguration(new RefrigeratorConfiguration());
            modelBuilder.ApplyConfiguration(new ExternalEnvoicesConfiguration());
            modelBuilder.ApplyConfiguration(new CoolingRoomsConfiguration());
            modelBuilder.ApplyConfiguration(new OtherSalesConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierOfFarmsConfiguration());
            modelBuilder.ApplyConfiguration(new RefrigeratorDetailsConfiguration());

            modelBuilder.ApplyConfiguration(new BoFUserConfiguration());
            modelBuilder.ApplyConfiguration(new BoFTotalConfiguration());
            modelBuilder.ApplyConfiguration(new BoFOperationsConfiguration());
            modelBuilder.ApplyConfiguration(new BoFOpDetailsConfiguration());

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // uncomment to start database logger 
            //var lf = new LoggerFactory();
            //lf.AddProvider(new MyLoggerProvider());
            //optionsBuilder.UseLoggerFactory(lf);

            optionsBuilder.UseLazyLoadingProxies();            
            base.OnConfiguring(optionsBuilder);
        }
    }
}
