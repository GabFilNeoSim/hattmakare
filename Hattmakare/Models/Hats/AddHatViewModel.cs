using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Models.Hats;

public class AddHatViewModel
{
    [Required(ErrorMessage = "Skriv in ett name")]
    [StringLength(100)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Skriv in en storlek")]
    [Range(0d, double.MaxValue, ErrorMessage = "Storleken måste vara 0 eller större")]
    public int Size { get; set; }

    [Required(ErrorMessage = "Skriv in en längd")]
    [Range(0d, double.MaxValue, ErrorMessage = "Längden måste vara 0 eller större")]
    public double Length { get; set; }

    [Required(ErrorMessage = "Skriv in en brädd")]
    [Range(0d, double.MaxValue, ErrorMessage = "Brädden måste vara 0 eller större")]
    public double Width { get; set; }

    [Required(ErrorMessage = "Skriv in djupet")]
    [Range(0d, double.MaxValue, ErrorMessage = "Djupet måste vara 0 eller större")]
    public double Depth { get; set; }

    [Required(ErrorMessage = "Skriv in ett pris")]
    [Range(0d, double.MaxValue, ErrorMessage = "Priset måste vara 0 eller större")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Skriv in ett antal i lager")]
    public int Quantity { get; set; }

    public string? Comment { get; set; }
    public IFormFile? Image { get; set; }

    public bool IsSpecial { get; set; } = false;

    public string? Controller { get; set; }
    public string? Action { get; set; }

    public List<MaterialQuantityViewModel> SelectedMaterials { get; set; } = [];
    public List<MaterialQuantityViewModel> AvailableMaterials { get; set; } = [];
}
