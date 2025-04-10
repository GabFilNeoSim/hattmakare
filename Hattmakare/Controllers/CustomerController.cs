using Hattmakare.Data;
using Hattmakare.Models.Customer;
using Microsoft.AspNetCore.Mvc;

namespace Hattmakare.Controllers;

[Route("customers")]
public class CustomerController : Controller
{
    private readonly AppDbContext _context;

    public CustomerController(AppDbContext context)
    {
        _context = context;
    }

    // Visa kundsidan
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    // Lägg till kund
    [HttpPost("add")]
    public async Task<IActionResult> AddCustomer(AddCustomerViewModel newCustomer)
    {
        throw new NotImplementedException();
    }

    // Ta bort en kund
    [HttpPost("remove/{customerId:int}")]
    public async Task<IActionResult> RemoveCustomer(int customerId)
    {
        throw new NotImplementedException();
    }

    // Uppdatera en kund
    [HttpPost("update/{customerId:int}")]
    public async Task<IActionResult> UpdateCustomer(int customerId, UpdateCustomerViewModel updateCustomer)
    {
        throw new NotImplementedException();
    }
}
