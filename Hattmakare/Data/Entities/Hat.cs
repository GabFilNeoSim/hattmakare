using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Data.Entities
{
    public class Hat
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        public int Price { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string? Comment { get; set; }

        public int Size { get; set; }

        public int InStock {  get; set; }

        public bool IsSpecial { get; set; } = false;

        public string? ImageUrl { get; set; }
        // Navigation props
        public virtual ICollection<HatMaterial> HatMaterials { get; set; }
        public virtual ICollection<OrderHat> OrderHats { get; set; }
    }
}
