using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

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

        //Navigation props
        public virtual ICollection<OrderHat> OrderHats { get; set; }
        public virtual ICollection<HatMaterial> HatMaterials { get; set; }

    }
}
