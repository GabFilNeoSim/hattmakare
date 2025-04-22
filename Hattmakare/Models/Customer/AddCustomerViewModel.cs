using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Models.Customer;

public class AddCustomerViewModel
{   
    [Required(ErrorMessage = "Vänligen ange ett förnamn")]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Vänligen ange ett efternamn")]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Vänligen ange ett huvudmått")]
    public double HeadMeasurements { get; set; }



    [Required(ErrorMessage = "Vänligen ange en faktureringsadress")]
    [MaxLength(100)]
    public string BillingAddress { get; set; }

    [Required(ErrorMessage = "Vänligen ange en leveransadress ")]
    [MaxLength(100)]
    public string DeliveryAddress { get; set; }

    [Required(ErrorMessage = "Vänligen ange ett land")]
    [MaxLength(100)]
    public string? City { get; set; }

    [Required(ErrorMessage = "Vänligen ange en postnummer")]
    [MaxLength(20)]
    public string PostalCode { get; set; }

    [Required(ErrorMessage = "Vänligen ange ett land")]
    [MaxLength(100)]
    public string Country { get; set; }

    [Required(ErrorMessage = "Vänligen ange en epost")]
    [EmailAddress(ErrorMessage = "Epost har ett ogiltigt format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Vänligen ange ett telefonnummer")]
    [Phone]
    public string Phone { get; set; }
}
