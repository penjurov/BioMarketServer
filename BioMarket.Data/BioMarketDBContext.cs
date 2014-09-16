namespace BioMarket.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using BioMarket.Data.Migrations;
    using BioMarket.Models;

    public class BioMarketDBContext : DbContext, IBioMarketDBContext
    {
        public BioMarketDBContext()
            : base("BioMarketConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BioMarketDBContext, Configuration>());
        }

        public IDbSet<Farm> Farms { get; set; }

        public IDbSet<Client> Clients { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Offer> Offers { get; set; }

        public IDbSet<Account> Accounts { get; set; }

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
