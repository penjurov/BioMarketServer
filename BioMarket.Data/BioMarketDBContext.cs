namespace BioMarket.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using BioMarket.Data.Migrations;
    using BioMarket.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class BioMarketDBContext : IdentityDbContext<Account>, IBioMarketDBContext
    {
        public BioMarketDBContext()
            : base("BioMarketConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BioMarketDBContext, Configuration>());
        }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Offer> Offers { get; set; }

        public IDbSet<Farm> Farms { get; set; }

        public IDbSet<Client> Clients { get; set; }

        public static BioMarketDBContext Create()
        {
            return new BioMarketDBContext();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public new void Dispose()
        {
            base.Dispose();
        }
    }
}
