namespace BioMarket.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using BioMarket.Models;

    public interface IBioMarketDBContext
    {
        IDbSet<Offer> Offers { get; set; }

        IDbSet<Farm> Farms { get; set; }

        IDbSet<Client> Clients { get; set; }

        IDbSet<Product> Products { get; set; }

        IDbSet<Account> Accounts { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();

        void Dispose();
    }
}
