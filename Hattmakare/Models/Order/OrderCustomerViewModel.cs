using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Models.Order
{
  public class OrderCustomerViewModel
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double HeadMeasurements { get; set; }
    public string BillingAddress { get; set; }
    public string DeliveryAddress { get; set; }
    public string? City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
  }
}
