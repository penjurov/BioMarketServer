namespace BioMarket.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Offer
    {
        public int Id { get; set; }

        [Required]
        public virtual Product Product { get; set; }

        public int ProductId { get; set; }

        [Required]
        public double Quantity { get; set; }

        public string ProductPhoto { get; set; }

        public virtual Client BoughtBy { get; set; }

        public int BoughtById { get; set; }

        [Required]
        public DateTime PostDate { get; set; }

        public DateTime BoughtDate { get; set; }

        public bool Deleted { get; set; }
    }
}
