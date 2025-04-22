using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hattmakare.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; } = false;
        public double HeadMeasurements { get; set; }

        // Foreign keys
        public int? AddressId { get; set; }

        // Navigation props
        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
