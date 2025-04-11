using Hattmakare.Data.Entities;

namespace Hattmakare.Models.Customer
{
    public class CustomerViewModel
    {
        public List<Data.Entities.Customer> customers { get; set; }

        public AddCustomerViewModel AddCustomer { get; set; }
    }
}
