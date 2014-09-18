namespace BioMarket.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;

    using BioMarket.Models;

    public class OfferModel
    {
        public static Expression<Func<Offer, OfferModel>> FromOffer
        {
            get
            {
                return a => new OfferModel
                {
                    Id = a.Id,
                    Quantity = a.Quantity,
                    ProductPhoto = a.ProductPhoto,
                    PostDate = a.PostDate
                };
            }
        }

        public static Expression<Func<Offer, OfferModel>> FromOfferWithProductAndBoughtBuy
        {
            get
            {
                return a => new OfferModel
                {
                    Id = a.Id,
                    Quantity = a.Quantity,
                    ProductPhoto = a.ProductPhoto,
                    PostDate = a.PostDate,
                    BoughtBy = new ClientModel
                    {
                        Account = a.BoughtBy.Account,
                        FirstName = a.BoughtBy.FirstName,
                        LastName = a.BoughtBy.LastName
                    },
                    BoughtDate = a.BoughtDate,
                    Product = new ProductModel
                    {
                        Id = a.Product.Id,
                        Name = a.Product.Name,
                        Price = a.Product.Price
                    }
                };
            }
        }

        public int Id { get; set; }

        public ProductModel Product { get; set; }

        [Required]
        public double Quantity { get; set; }

        public string ProductPhoto { get; set; }

        public ClientModel BoughtBy { get; set; }

        [Required]
        public DateTime PostDate { get; set; }

        public DateTime? BoughtDate { get; set; }

        public bool Deleted { get; set; }
    }
}