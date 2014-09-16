namespace BioMarket.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {   
        private ICollection<Offer> offers;

        public Product()
        {
            this.offers = new HashSet<Offer>();

        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public bool Deleted { get; set; }

        [Required]
        public virtual Account Farm { get; set; }

        public int FarmId { get; set; }

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
