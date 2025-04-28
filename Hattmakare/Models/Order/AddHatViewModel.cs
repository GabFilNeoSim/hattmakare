using Hattmakare.Models.Hats;

namespace Hattmakare.Models.Order
{
  public class AddHatViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Size { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public double Depth { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? Comment { get; set; }
    public string? ImageUrl { get; set; }
    public int HatTypeId { get; set; }
    public List<MaterialQuantityViewModel> Materials { get; set; }
  }
}
