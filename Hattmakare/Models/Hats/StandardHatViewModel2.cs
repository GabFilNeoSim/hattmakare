namespace Hattmakare.Models.Hats;

public class StandardHatViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal? Price { get; set; }
    public string? Comment { get; set; }
    public int? Size { get; set; }
    public int? Quantity { get; set; }
    public bool? IsDeleted { get; set; }
    public string? ImageUrl { get; set; }
}
