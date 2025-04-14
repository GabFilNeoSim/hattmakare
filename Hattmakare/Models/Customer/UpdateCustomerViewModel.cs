using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Models.Customer;

public class UpdateCustomerViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter a firstname")]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Please enter a lastname")]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Please enter a street address")]
    [MaxLength(100)]
    public string StreetAddress { get; set; }

    [Required(ErrorMessage = "Please enter a city")]
    [MaxLength(100)]
    public string City { get; set; }

    [Required(ErrorMessage = "Please enter a postal code")]
    [MaxLength(20)]
    public string PostalCode { get; set; }

    [Required(ErrorMessage = "Please enter a country")]
    [MaxLength(100)]
    public string Country { get; set; }

    [Required(ErrorMessage = "Please enter an email")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Please enter a phone number")]
    [Phone]
    public string Phone { get; set; }
}
