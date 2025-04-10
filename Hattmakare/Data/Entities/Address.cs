using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Data.Entities
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string StreetAddress { get; set; }

        [Required]
        [MaxLength(20)]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength (100)]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        // Navigation props
        public virtual ICollection<Customer> Customers { get; set; }
    }
}