namespace Hattmakare.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public bool Priority { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal Price { get; set; }
        public string Customer {  get; set; }
        public string Status { get; set; }
        public List<string> Managers { get; set; }
    }
}