namespace BioMarket.Web.Models
{
    using System;
    using System.Linq.Expressions;

    using BioMarket.Models;
    using System.ComponentModel.DataAnnotations;

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
                    Price = p.Price,
                    FarmId = p.FarmId
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int FarmId { get; set; }

        public bool Deleted { get; set; }
    }
}