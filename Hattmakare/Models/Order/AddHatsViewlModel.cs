namespace Hattmakare.Models.Order
{
  public class AddHatsViewlModel
  {
    public int OrderId { get; set; }
    public List<AddHatViewModel> Hats { get; set; } = new();
  }
}
