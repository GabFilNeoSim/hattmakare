using Hattmakare.Models.Hats;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hattmakare.Models.Order
{
    public class OrderListViewModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public List<OrderViewModel> Orders {  get; set; }
      

    }
}
