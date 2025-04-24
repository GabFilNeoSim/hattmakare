using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Hattmakare.Data.Entities;

namespace Hattmakare.Models.Order
{
    public class MaterialOrderViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Unit { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(50)]
        public string Supplier { get; set; }

        [Required]
        public bool IsDecoration { get; set; }

        public List<OrderHat> OrderHats { get; set; } = new();

        public int OrderId { get; set; }
     
        public List<HatMaterial> HatMaterials { get; set; } = new();
    }
}
