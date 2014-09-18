namespace BioMarket.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
<<<<<<< HEAD

    using BioMarket.Models;
=======
    using System.Web;
>>>>>>> 9d8de2b70726ab5fe6606c26ba72067bbbf62004

    using BioMarket.Models;

    public class OfferModel
    {
        public static Expression<Func<Offer, OfferModel>> FromOffer
        {
            get
            {
<<<<<<< HEAD
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
=======
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
>>>>>>> 9d8de2b70726ab5fe6606c26ba72067bbbf62004
                };
            }
        }

        public int Id { get; set; }

<<<<<<< HEAD
        public ProductModel Product { get; set; }

        [Required]
=======
        public virtual ProductModel Product { get; set; }

        public int ProductId { get; set; }

>>>>>>> 9d8de2b70726ab5fe6606c26ba72067bbbf62004
        public double Quantity { get; set; }

        public string ProductPhoto { get; set; }

<<<<<<< HEAD
        public ClientModel BoughtBy { get; set; }

        [Required]
        public DateTime PostDate { get; set; }

        public DateTime? BoughtDate { get; set; }

        public bool Deleted { get; set; }
=======
        public virtual Account BoughtBy { get; set; }

        public int BoughtById { get; set; }

        public DateTime PostDate { get; set; }

        public DateTime BoughtDate { get; set; }
>>>>>>> 9d8de2b70726ab5fe6606c26ba72067bbbf62004
    }
}