using Hattmakare.Models.Hats;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hattmakare.Models.Order
{
    public class OrderIndexViewModel
    {
        public List<OrderListViewModel> OrderItems { get; set; }
        public List<HatViewModel> Hats { get; set; }
        public List<SelectListItem> HatNames { get; set; }
        public int HatId { get; set; }
    }
}
