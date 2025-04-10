using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Data.Entities
{
    public class OrderStatus
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        // Navigation
        public virtual ICollection<Order> Orders { get; set; }
    }
}
