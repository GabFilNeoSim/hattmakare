using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hattmakare.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public bool Priority { get; set; } = false;
        [Required]
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal Price { get; set; }
       

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
