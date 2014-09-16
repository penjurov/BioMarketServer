namespace BioMarket.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Farm
    {
        private ICollection<Product> products;

        public Farm()
        {
            this.products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public string Phones { get; set; }

        public string Owner { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public bool Deleted { get; set; }

        [Required]
        public virtual Account Account { get; set; }

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
    }
}
