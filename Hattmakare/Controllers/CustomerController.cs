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
        var customers = await _context.Customers.ToListAsync();

        var viewModel = new CustomerViewModel
        {
            customers = customers,

            AddCustomer = new AddCustomerViewModel()
        };

        return View(viewModel);
    }

    // Lägg till kund
    [HttpPost("add")]
    public async Task<IActionResult> AddCustomer(AddCustomerViewModel newCustomer)
    {

        if (!ModelState.IsValid)
        {
            var customers = await _context.Customers.ToListAsync();
            var viewModel = new CustomerViewModel
            {
                customers = customers,
                AddCustomer = new AddCustomerViewModel()
            };

            return View("Index", viewModel);
        }

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

    // Ta bort en kund
    [HttpPost("remove/{customerId:int}")]
    public async Task<IActionResult> RemoveCustomer(int customerId)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId);

        _context.Customers.Remove(customer);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ett fel inträffade: {ex.Message}");
        }

        return RedirectToAction("Index");
    }

    // Uppdatera en kund
    [HttpPost("update/{customerId:int}")]
    public async Task<IActionResult> UpdateCustomer(int customerId, UpdateCustomerViewModel updateCustomer)
    {
        throw new NotImplementedException();
    }

    [HttpPost("search/{customerId:int}")]
    
    // Söka efter en kund
    public async Task<IActionResult> SearchCustomer(int customerId, CustomerViewModel customerViewModel)
    {
        throw new NotImplementedException();
    }
}
