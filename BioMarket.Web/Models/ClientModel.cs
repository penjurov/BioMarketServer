namespace BioMarket.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;

    using BioMarket.Models;

    public class ClientModel
    {
        public static Expression<Func<Client, ClientModel>> FromClient
        {
            get
            {
                return a => new ClientModel
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Phone = a.Phone,
                    Account = a.Account
                };
            }
        }

        public static Expression<Func<Client, ClientModel>> FromClientWithOffers
        {
            get
            {
                return a => new ClientModel
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Phone = a.Phone,
                    Account = a.Account,
                    Offers = a.Offers.Select(s => new OfferModel
                    {
                        Id = s.Id,
                        Product = new ProductModel
                        {
                            Name = s.Product.Name,                            
                        },
                        BoughtDate = s.BoughtDate,
                        Quantity = s.Quantity
                    })
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Account { get; set; }

        public IEnumerable<OfferModel> Offers { get; set; }
    }
}