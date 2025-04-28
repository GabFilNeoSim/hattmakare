using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hattmakare.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public bool Priority { get; set; } = false;
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
       
        public int? DiscountPercentage { get; set; }

        // Foreign keys
        public int? CustomerId { get; set; }  
        public int? OrderStatusId { get; set; }

        // Navigation props
        public virtual ICollection<OrderHat> OrderHats { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        [ForeignKey(nameof(OrderStatusId))]
        public virtual OrderStatus OrderStatus { get; set; }
    }
}
