namespace BioMarket.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;

    using BioMarket.Models;

    public class FarmModel
    {
        public static Expression<Func<Farm, FarmModel>> FromFarm
        {
            get
            {
                return a => new FarmModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Address = a.Address,
                    Phones = a.Phones,
                    Owner = a.Owner,
                    Longitude = a.Longitude,
                    Latitude = a.Latitude,
                    Account = a.Account
                };
            }
        }

        public static Expression<Func<Farm, FarmModel>> FromFarmWithProducts
        {
            get
            {
                return a => new FarmModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Address = a.Address,
                    Phones = a.Phones,
                    Owner = a.Owner,
                    Longitude = a.Longitude,
                    Latitude = a.Latitude,
                    Account = a.Account,
                    Products = a.Products.Select(s => new ProductModel
                    {
                        // Id = s.Id
                    })
                };
            }
        }

        public int Id { get; set; }

        public string Account { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public string Phones { get; set; }

        public string Owner { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public bool Deleted { get; set; }

        public virtual IEnumerable<ProductModel> Products { get; set; }
    }
}