namespace BioMarket.Models
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public class Account : IdentityUser
    {
        private ICollection<Offer> offers;
        private ICollection<Product> products;

        public Account()
        {
            this.offers = new HashSet<Offer>();
            this.products = new HashSet<Product>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Account> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string FarmName { get; set; }

        public string FarmAddress { get; set; }

        public string FarmPhones { get; set; }

        public string FarmOwner { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<Product> Products 
        { 
            get
            {
                return this.products;
            }

            set
            {
                this.products = value;
            }
        }

        public virtual ICollection<Offer> Offers 
        { 
            get
            {
                return this.offers;
            }

            set
            {
                this.offers = value;
            }
        }
    }
}
