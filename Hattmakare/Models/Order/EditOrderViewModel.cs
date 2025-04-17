using Hattmakare.Models.Hats;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hattmakare.Models.Order;

public class EditOrderViewModel
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool Priority { get; set; }

    public List<EditOrderHatViewModel> Hats { get; set; }
    public List<SelectListItem> Users { get; set; }
}
