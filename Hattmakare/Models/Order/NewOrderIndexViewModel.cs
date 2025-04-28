using Hattmakare.Models.Customer;
using Hattmakare.Models.Hats;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hattmakare.Models.Order
{
    public class NewOrderIndexViewModel
    {
        public int CustomerId { get; set; }
        public int OrderStatusId { get; set; } = 1;
        public CustomerViewModel Customer { get; set; }

        public List<MaterialQuantityViewModel> AvailableMaterials { get; set; } = new();

        public NewOrderViewModel NewOrders { get; set; }

        public AddCustomerViewModel AddCustomer { get; set; }

        public List<HatViewModel> Hats { get; set; }

        public List<SelectListItem> Customers { get; set; } //Dropdown lista
    }
}
