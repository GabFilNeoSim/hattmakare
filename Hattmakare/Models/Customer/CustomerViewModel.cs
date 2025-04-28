using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Models.Customer;

public class CustomerViewModel
{
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Vänligen ange ett förnamn")]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Vänligen ange ett efternamn")]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Vänligen ange ett telefonnummer")]
    [Phone]
    public string Phone { get; set; }

    public bool IsDeleted { get; set; } = false;
}