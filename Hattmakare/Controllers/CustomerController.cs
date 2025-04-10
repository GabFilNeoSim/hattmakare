using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hattmakare.Controllers;

[Route("customers")]
public class CustomerController : Controller
{
    [HttpGet] // Show customer page
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("add")]
    public IActionResult AddCustomer(object customer)
    {
        throw new NotImplementedException();
    }
}
