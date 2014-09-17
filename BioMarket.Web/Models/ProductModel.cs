namespace BioMarket.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Linq.Expressions;

    using BioMarket.Models;

    public class ProductModel
    {
        public static Expression<Func<Product, ProductModel>> FromFarm
        {
            get
            {
                return a => new ProductModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Price = a.Price,
                    Deleted = a.Deleted,
                    FarmId = a.FarmId
                };
            }
        }

        public static Expression<Func<Product, ProductModel>> FromFarmWithOffers
        {
            get
            {
                return a => new ProductModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Price = a.Price,
                    Deleted = a.Deleted,
                    FarmId = a.FarmId,
                    Offers = a.Offers.Select(o => new OfferModel()
                    {
                        BoughtBy = o.BoughtBy,
                        BoughtDate = o.BoughtDate
                    })
                };
            }
        }


        private ICollection<Offer> offers;

        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public bool Deleted { get; set; }

        public virtual Account Farm { get; set; }

        public int FarmId { get; set; }

        public virtual IEnumerable<OfferModel> Offers { get; set; }
    }
}