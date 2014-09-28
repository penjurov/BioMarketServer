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
                    PostDate = a.PostDate,
                    Product = a.Product.Name,
                    ProductId = a.ProductId,
                    Farm = a.Product.Farm.Name,
                    FarmId = a.Product.FarmId,
                    FarmLatitude = a.Product.Farm.Latitude,
                    FarmLongitude = a.Product.Farm.Longitude
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
                    ProductId = a.ProductId
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public string Product { get; set; }

        public int FarmId { get; set; }

        public string Farm { get; set; }     

        [Required]
        public double Quantity { get; set; }

        public string ProductPhoto { get; set; }

        public ClientModel BoughtBy { get; set; }

        public DateTime PostDate { get; set; }

        public DateTime? BoughtDate { get; set; }

        public decimal FarmLatitude { get; set; }

        public decimal FarmLongitude { get; set; }

        public bool Deleted { get; set; }
    }
}