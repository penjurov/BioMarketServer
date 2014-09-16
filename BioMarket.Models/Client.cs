namespace BioMarket.Models
{
    using System.Collections.Generic;

    public class Client
    {
        private ICollection<Offer> offers;

        public Client()
        {
            this.offers = new HashSet<Offer>();          
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Account { get; set; }

        public bool Deleted { get; set; }

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
