namespace BioMarket.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    using BioMarket.Models;

    public class OfferModel
    {
        public static Expression<Func<Offer, OfferModel>> FromOffer
        {
            get
            {
                return o => new OfferModel
                {
                    Id = o.Id,
                    ProductId = o.ProductId,
                    Quantity = o.Quantity,
                    ProductPhoto = o.ProductPhoto,
                    BoughtBy = o.BoughtBy,
                    BoughtById = o.BoughtById,
                    PostDate = o.PostDate,
                    BoughtDate = o.BoughtDate
                };
            }
        }

        public int Id { get; set; }

        public virtual ProductModel Product { get; set; }

        public int ProductId { get; set; }

        public double Quantity { get; set; }

        public string ProductPhoto { get; set; }

        public virtual Account BoughtBy { get; set; }

        public int BoughtById { get; set; }

        public DateTime PostDate { get; set; }

        public DateTime BoughtDate { get; set; }
    }
}