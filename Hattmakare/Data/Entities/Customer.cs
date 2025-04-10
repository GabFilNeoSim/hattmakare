using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        // Navigation props
        public virtual ICollection<PhoneNumbers> PhoneNumbers { get; set; }
        public virtual ICollection<CustomerAddress> Addresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
