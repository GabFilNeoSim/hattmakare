using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hattmakare.Data.Entities
{
    public class HatMaterial
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        // Foreign keys
        public int HatId { get; set; }
        public int Materialid { get; set; }

        // Navigation props
        [ForeignKey(nameof(HatId))]
        public virtual Hat Hat { get; set; }
        [ForeignKey(nameof(Materialid))]
        public virtual Material Material { get; set; }
    }
}
