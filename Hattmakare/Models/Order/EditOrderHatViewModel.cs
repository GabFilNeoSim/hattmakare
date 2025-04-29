using Hattmakare.Models.Hats;
using Hattmakare.Models.Material;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hattmakare.Models.Order;

public class EditOrderHatViewModel
{
    public int HatId { get; set; }
    public string HatName { get; set; }
    public string HatImageUrl { get; set; }
    public decimal Price { get; set; }
    public int? Size { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public double Depth { get; set; }
  public string? UserId { get; set; }

  public string Comment { get; set; }
  public List<OrderHatMaterialViewModel> Materials { get; set; }
}
