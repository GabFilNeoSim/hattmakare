namespace Hattmakare.Models.Order
{
    public class OrderListViewModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public List<OrderViewModel> Orders {  get; set; }
    }
}
