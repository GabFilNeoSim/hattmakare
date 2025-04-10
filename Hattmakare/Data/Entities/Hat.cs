using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Data.Entities
{
    public class Hat
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public int Price { get; set; }

        // Navigation props
        public virtual ICollection<HatMaterial> HatMaterials { get; set; }
    }
}
