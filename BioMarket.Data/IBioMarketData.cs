namespace BioMarket.Data
{
    using BioMarket.Data.Repositories;
    using BioMarket.Models;

    public interface IBioMarketData
    {
        IGenericRepository<Account> Accounts { get; }

        IGenericRepository<Farm> Farms { get; }

        IGenericRepository<Client> Clients { get; }

        IGenericRepository<Product> Products { get; }

        IGenericRepository<Offer> Offers { get; }

        void SaveChanges();

        void Dispose();
    }
}
