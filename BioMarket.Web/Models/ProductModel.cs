namespace BioMarket.Web.Models
{
    using System;
<<<<<<< HEAD
=======
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
>>>>>>> 9d8de2b70726ab5fe6606c26ba72067bbbf62004
    using System.Linq.Expressions;

    using BioMarket.Models;

    public class ProductModel
    {
<<<<<<< HEAD
        public static Expression<Func<Product, ProductModel>> FromProduct
        {
            get
            {
                return p => new ProductModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
=======
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
>>>>>>> 9d8de2b70726ab5fe6606c26ba72067bbbf62004
                };
            }
        }

<<<<<<< HEAD
=======
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

>>>>>>> 9d8de2b70726ab5fe6606c26ba72067bbbf62004
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public bool Deleted { get; set; }
<<<<<<< HEAD
=======

        public virtual Account Farm { get; set; }

        public int FarmId { get; set; }

        public virtual IEnumerable<OfferModel> Offers { get; set; }
>>>>>>> 9d8de2b70726ab5fe6606c26ba72067bbbf62004
    }
}