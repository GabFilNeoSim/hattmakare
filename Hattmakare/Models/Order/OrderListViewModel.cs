using Hattmakare.Models.Hats;

namespace Hattmakare.Models.Order
{
    public class OrderListViewModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public List<OrderViewModel> Orders {  get; set; }
        public List<HatViewModel> Hats { get; set; }
    }
}
