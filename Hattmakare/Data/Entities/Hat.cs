using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Hattmakare.Data.Entities
{
    public class Hat
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        public string? Comment { get; set; }
        public string? ImageUrl { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsSpecial { get; set; } = false;
        public int? Size { get; set; }
        public int? Quantity { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }

        //Navigation props
        public virtual ICollection<OrderHat> OrderHats { get; set; }
        public virtual ICollection<HatMaterial> HatMaterials { get; set; }

    }
}
