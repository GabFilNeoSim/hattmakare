using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Castle.Core.Resource;
using Hattmakare.Data;
using Hattmakare.Data.Entities;
using Hattmakare.Models.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Hattmakare.Controllers;

[Authorize]
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
                c.LastName.ToLower().Contains(query) && c.IsDeleted == false);
        }

        else
        {
            customers = _context.Customers.Where(c => c.IsDeleted == false);
        }

        // Sortera kunderna i alfabetisk ordning baserat på förnamn och efternamn
        var sortedCustomers = await customers
            .OrderBy(c => c.FirstName)
            .ToListAsync();

        var viewModel = new CustomersViewModel
        {
            Customers = sortedCustomers.Select(c => new CustomerViewModel
            {
                CustomerId = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Phone = c.PhoneNumber
            }).ToList(),
            AddCustomer = new AddCustomerViewModel()
        };


        return View(viewModel);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddCustomer(
    string firstName,
    string lastName,
    string email,
    string headMeasurements,
    string billingAddress,
    string deliveryAddress,
    string city,
    string postalCode,
    string country,
    string phone 
)
    {
        bool isValid = true;

        if (string.IsNullOrWhiteSpace(firstName))
        {
            ViewData["FirstNameError"] = "Förnamn är obligatoriskt.";
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            ViewData["LastNameError"] = "Efternamn är obligatoriskt.";
            isValid = false;
        }

       
        if (string.IsNullOrWhiteSpace(headMeasurements))
        {
            ViewData["HeadMeasurementsError"] = "Ange ett giltigt huvudmått.";
            isValid = false;
        }

      
        if (string.IsNullOrWhiteSpace(billingAddress))
        {
            ViewData["BillingAddressError"] = "Faktureringsadress är obligatorisk.";
            isValid = false;
        }

       
        if (string.IsNullOrWhiteSpace(deliveryAddress))
        {
            ViewData["DeliveryAddressError"] = "Leveransadress är obligatorisk.";
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            ViewData["CityError"] = "Stad är obligatoriskt.";
            isValid = false;
        }

        
        if (!Regex.IsMatch(postalCode ?? "", @"^\d{5}$"))
        {
            ViewData["PostalCodeError"] = "Postnummer måste vara fem siffror.";
            isValid = false;
        }

        
        if (string.IsNullOrWhiteSpace(country))
        {
            ViewData["CountryError"] = "Land är obligatoriskt.";
            isValid = false;
        }

        if (!Regex.IsMatch(email ?? "", @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
        {
            ViewData["EmailError"] = "Ogiltig e-postadress. Kontrollera formatet.";
            isValid = false;
        }

        var phoneRegex = @"^\+?\d{7,15}$"; 

        if (string.IsNullOrWhiteSpace(phone) || !Regex.IsMatch(phone, phoneRegex))
        {
            ViewData["PhoneError"] = "Ogiltigt telefonnummer. Ange 7-15 siffror.";
            isValid = false;
        }

        if (!isValid)
        {
            var customers = await _context.Customers.ToListAsync();
            var model = new CustomersViewModel  
            {
                AddCustomer = new AddCustomerViewModel()
                
            };

            return View("Index", model);
        }

        
        var customer = new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            HeadMeasurements = double.Parse(headMeasurements),
            Email = email,
            PhoneNumber = phone,

            Address = new Address

            {
                BillingAddress = billingAddress,
                DeliveryAddress = deliveryAddress,
                City = city,
                PostalCode = postalCode,
                Country = country,

            }
            
        };

        await _context.Customers.AddAsync(customer);

        await _context.SaveChangesAsync();

        TempData["NotifyType"] = "success";
        TempData["NotifyMessage"] = "Kunden sparades!";

        return RedirectToAction("Index");
    }


    // Ta bort en kund
    [HttpPost("remove/{customerId:int}")]
    public async Task<IActionResult> RemoveCustomer(int customerId)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId);

        customer.IsDeleted = true;

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
        customer.HeadMeasurements = updateCustomer.HeadMesurements;
        customer.PhoneNumber = updateCustomer.Phone;
        customer.Email = updateCustomer.Email;

        if (customer.Address == null)
        {
            customer.Address = new Address();
        }

        customer.Address.BillingAddress = updateCustomer.BillingAddress;
        customer.Address.DeliveryAddress = updateCustomer.DeliveryAddress;
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
            HeadMesurements = customer.HeadMeasurements,
            Email = customer.Email,
            Phone = customer.PhoneNumber,
            BillingAddress = customer.Address?.BillingAddress,
            DeliveryAddress = customer.Address?.DeliveryAddress,
            City = customer.Address?.City,
            PostalCode = customer.Address?.PostalCode,
            Country = customer.Address?.Country
        };

        return View(viewModel);
    }
}
