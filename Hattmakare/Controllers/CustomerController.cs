using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using Castle.Core.Resource;
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
    public async Task<IActionResult> Index(string? query)
    {
        var customers = _context.Customers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            query = query.ToLower();
            customers = customers.Where(c =>
                (c.FirstName + " " + c.LastName).ToLower().Contains(query) ||
                c.FirstName.ToLower().Contains(query) ||
                c.LastName.ToLower().Contains(query));
        }

        var result = await customers.ToListAsync();

        var viewModel = new CustomerViewModel
        {
            customers = result,
            AddCustomer = new AddCustomerViewModel()
        };

        return View(viewModel);
    }

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
    [HttpPost("edit/{customerId:int}")]
    public async Task<IActionResult> UpdateCustomer(int customerId, UpdateCustomerViewModel updateCustomer)
    {
        if (!ModelState.IsValid)
        {
            return View(updateCustomer);
        }

        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId);

        if(customer == null)
        {
            return NotFound();
        }

        customer.FirstName = updateCustomer.FirstName;
        customer.LastName = updateCustomer.LastName;
        customer.PhoneNumber = updateCustomer.Phone;
        customer.Email = updateCustomer.Email;

        if (customer.Address == null)
        {
            customer.Address = new Address();
        }

        customer.Address.StreetAddress = updateCustomer.StreetAddress;
        customer.Address.City = updateCustomer.City;
        customer.Address.PostalCode = updateCustomer.PostalCode;
        customer.Address.Country = updateCustomer.Country;

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    //Visa formuläret för uppdatering
    [HttpGet("edit/{customerId:int}")]
    public async Task<IActionResult> UpdateCustomer(int customerId)
    {
        var customer = await _context.Customers.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == customerId);

        var viewModel = new UpdateCustomerViewModel
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.PhoneNumber,
            StreetAddress = customer.Address?.StreetAddress,
            City = customer.Address?.City,
            PostalCode = customer.Address?.PostalCode,
            Country = customer.Address?.Country
        };

        return View(viewModel);
    }
}
