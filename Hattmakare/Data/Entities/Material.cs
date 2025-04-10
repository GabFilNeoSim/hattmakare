using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hattmakare.Data.Entities
{
    public class Material
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Unit { get; set; }

        [Required]
        public int PurchasePrice { get; set; }

        [Required]
        public string Supplier { get; set; }

        // Navigation props
        public virtual ICollection<HatMaterial> HatMaterials { get; set; }
    }
}
