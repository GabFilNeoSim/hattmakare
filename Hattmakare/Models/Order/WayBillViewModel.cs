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

    public decimal totalPrice => price + tax;

    public Data.Entities.Order order { get; set; }
    public int orderNumber { get; set; }

    public List<OrderHat> OrderHats { get; set; } = new();

    //public List<User> Users { get; set; }

    //public List<Hattmakare.Data.Entities.Customer> customers { get; set; }


}


