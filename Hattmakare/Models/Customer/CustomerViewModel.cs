using Hattmakare.Data.Entities;

namespace Hattmakare.Models.Customer
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public List<Data.Entities.Customer> customers { get; set; }

        public UpdateCustomerViewModel UpdateCustomer { get; set; }

        public AddCustomerViewModel AddCustomer { get; set; }
    }
}
