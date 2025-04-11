using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using Hattmakare.Data;
using Hattmakare.Data.Entities;
using Hattmakare.Models.Customer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
    public async Task<IActionResult> Index()
    {
        var customers = await _context.Customers
            .ToListAsync();

        return View("Index");
    }

    // Lägg till kund
    [HttpPost("add")]
    public async Task<IActionResult> AddCustomer(AddCustomerViewModel newCustomer)
    {

        if (ModelState.IsValid)
        {
            var customer = new Customer
            {
                FirstName = newCustomer.FirstName,
                LastName = newCustomer.LastName,
                Email = newCustomer.Email,
                PhoneNumber = newCustomer.Phone,

                Address = new Address
                {
                    StreetAddress = newCustomer.StreetAddress,
                    PostalCode = newCustomer.PostalCode,
                    City = newCustomer.City,
                    Country = newCustomer.Country,

                }
            };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        
        }

        return View(AddCustomer);

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
