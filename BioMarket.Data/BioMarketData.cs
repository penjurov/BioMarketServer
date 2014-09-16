namespace BioMarket.Data
{
    using System;
    using System.Collections.Generic;

    using BioMarket.Data.Repositories;
    using BioMarket.Models;

    public class BioMarketData : IBioMarketData
    {
        private IBioMarketDBContext context;
        private IDictionary<Type, object> repositories;

        public BioMarketData(IBioMarketDBContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public BioMarketData()
            : this(new BioMarketDBContext())
        {
        }

        public IGenericRepository<Account> Accounts
        {
            get 
            { 
                return this.GetRepository<Account>();
            }
        }

        public IGenericRepository<Product> Products
        {
            get
            {
                return this.GetRepository<Product>();
            }
        }

        public IGenericRepository<Offer> Offers
        {
            get
            {
                return this.GetRepository<Offer>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}
