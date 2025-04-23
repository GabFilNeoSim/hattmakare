using System.ComponentModel.DataAnnotations.Schema;

namespace Hattmakare.Models.Hats;

public class HatViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Comment { get; set; }
    public string? ImageUrl { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public bool IsDeleted { get; set; } = false;
    public int? Size { get; set; }
    public int? Quantity { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public double Depth { get; set; }
    public int HatTypeId { get; set; }
    public string HatTypeName { get; set; }
}
