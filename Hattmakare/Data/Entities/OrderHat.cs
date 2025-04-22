using System.ComponentModel.DataAnnotations.Schema;

namespace Hattmakare.Data.Entities
{
    public class OrderHat
    {
        public int Id { get; set; }
        // Foreign keys
        public int HatId { get; set; }
        public int OrderId { get; set; }
        public string? UserId { get; set; }

        // Navigation props
        [ForeignKey(nameof(HatId))]
        public virtual Hat Hat { get; set; }
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
