namespace BioMarket.Web.Models
{
    using System;
    using System.Linq.Expressions;

    using BioMarket.Models;

    public class ProductModel
    {
        public static Expression<Func<Product, ProductModel>> FromProduct
        {
            get
            {
                return p => new ProductModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public Farm Farm { get; set; }

        public bool Deleted { get; set; }
    }
}