using Hattmakare.Models.Hats;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hattmakare.Models.Order
{
  public class NewOrderViewModel
  {
    public List<HatViewModel> Hats { get; set; }

    public List<SelectListItem> Customers { get; set; } //Dropdown lista
    public int CustomerId { get; set; } //Valt kund id
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool Priority { get; set; }
  }
}
