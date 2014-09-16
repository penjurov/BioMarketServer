namespace BioMarket.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class Client
    {
        private ICollection<Offer> offers;

        public Client()
        {
            this.offers = new HashSet<Offer>();
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Phone { get; set; }

        [Required]
        public virtual Account Account { get; set; }

        public int AccountId { get; set; }

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

        public bool Deleted { get; set; }
    }
}
