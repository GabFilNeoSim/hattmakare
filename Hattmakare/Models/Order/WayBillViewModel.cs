using Hattmakare.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hattmakare.Models.Order;


public class WayBilViewModel
{
    public string address { get; set; }
    public Data.Entities.User sender { get; set; }
    public Data.Entities.Customer reciver { get; set; }
    public int Export { get; set; }
    public decimal price { get; set; }
    public int weight { get; set; }

    public decimal tax => price * 0.25m;

    public bool IsPriority { get; set; }

    public decimal prio => IsPriority ? price * 1.20m : 0;

    public decimal totalPrice
    {
        get
        {
            decimal basePrice = price + tax + prio;

            if (DiscountPercentage is null)
            {
                return basePrice;
            }
            else
            {
                return basePrice * (1 - (DiscountPercentage.Value / 100m));
            }
        }
    }


    public int? DiscountPercentage { get; set; }

    public Data.Entities.Order order { get; set; }
    public int orderNumber { get; set; }

    public List<OrderHat> OrderHats { get; set; } = new();

    //public List<User> Users { get; set; }

    //public List<Hattmakare.Data.Entities.Customer> customers { get; set; }


}


