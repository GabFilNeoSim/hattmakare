using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Models.Customer;

public class UpdateCustomerViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vänligen ange ett förnamn")]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Vänligen ange ett efternamn")]

    public string LastName { get; set; }


    [Required(ErrorMessage = "Vänligen ange ett huvudmått")]

    public double HeadMesurements { get; set; }

    [Required(ErrorMessage = "Vänligen ange en faktureringsadress")]
    [MaxLength(100)]
    public string BillingAddress { get; set; }

    [Required(ErrorMessage = "Vänligen ange en leveransadress")]
    [MaxLength(100)]
    public string DeliveryAddress { get; set; }

    [Required(ErrorMessage = "Vänligen ange en stad")]
    [MaxLength(100)]
    public string City { get; set; }

    [Required(ErrorMessage = "Vänligen ange en postnummer")]
    [MaxLength(20)]
    public string PostalCode { get; set; }

    [Required(ErrorMessage = "Vänligen ange ett land")]
    [MaxLength(100)]
    public string Country { get; set; }

    [Required(ErrorMessage = "Vänligen ange en epost")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Vänligen ange ett giltigt telefonnummer")]
    [Phone]
    public string Phone { get; set; }
}
