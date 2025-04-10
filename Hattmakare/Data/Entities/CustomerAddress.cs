using System.ComponentModel.DataAnnotations.Schema;

namespace Hattmakare.Data.Entities
{
    public class CustomerAddress
    {
        public int Id { get; set; }

        // Foreign keys
        public int CustomerId { get; set; }
        public int AddressId { get; set; }

        // Navigation props
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; }
    }
}
