namespace Hattmakare.Models.Order
{
  public class AddOrderViewModel
  {
    public List<AddHatViewModel> Hats { get; set; } 
    public OrderCustomerViewModel Customer { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Priority { get; set; }
  }
}
