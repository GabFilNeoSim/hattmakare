using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Data.Entities;

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
    public decimal Price { get; set; }

    public string Supplier { get; set; }

    public bool IsDecoration { get; set; } = false;

    // Navigation props
    public virtual ICollection<HatMaterial> HatMaterials { get; set; }
}
