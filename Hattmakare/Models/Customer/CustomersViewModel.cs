using Hattmakare.Models.Customer;

namespace Hattmakare.Models.Customer
{
    public class CustomersViewModel
    {
        public int Id { get; set; }

        public List<CustomerViewModel> Customers { get; set; } 

        public UpdateCustomerViewModel UpdateCustomer { get; set; }

        public AddCustomerViewModel AddCustomer { get; set; }
    }
}
