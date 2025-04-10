using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hattmakare.Data.Entities
{
    public class HatMaterial
    {
        [Required]
        public int Quantity { get; set; }

        // Foreign keys
        public int HatId { get; set; }
        public int MaterialId { get; set; }

        // Navigation props
        [ForeignKey(nameof(HatId))]
        public virtual Hat Hat { get; set; }

        [ForeignKey(nameof(MaterialId))]
        public virtual Material Material { get; set; }
    }
}
