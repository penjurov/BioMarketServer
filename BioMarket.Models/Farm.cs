namespace BioMarket.Models
{
    using System.Collections.Generic;

    public class Farm
    {
        private ICollection<Product> products;

        public Farm()
        {
            this.products = new HashSet<Product>();            
        }

        public int Id { get; set; }

        public string Account { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phones { get; set; }

        public string Owner { get; set; }

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
    }
}
