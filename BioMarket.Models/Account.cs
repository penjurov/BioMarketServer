namespace BioMarket.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Account
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        public string Authentification { get; set; }

        public DateTime RegisterDate { get; set; }

        public virtual Client Client { get; set; }

        public int? ClientId { get; set; }

        public virtual Farm Farm { get; set; }

        public int? FarmId { get; set; }
    }
}
