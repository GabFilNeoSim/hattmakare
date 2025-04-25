using Hattmakare.Models.Hats;
using Hattmakare.Models.Material;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Models.Order
{
  public class NewOrderViewModel
  {
    public List<HatViewModel> Hats { get; set; }
    public List<MaterialQuantityViewModel> AvailableMaterials { get; set; } = new();
    public List<SelectListItem> Customers { get; set; } //Dropdown lista
    public int CustomerId { get; set; } //Valt kund id
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool Priority { get; set; }

    }
}
