using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Models.Auth;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Skriv in ett förnamn")]
    [RegularExpression("^[a-zA-ZåäöÅÄÖ]+$", ErrorMessage = "Endast bokstäver är tillåtna")]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Skriv in ett efternamn")]
    [RegularExpression("^[a-zA-ZåäöÅÄÖ]+$", ErrorMessage = "Endast bokstäver är tillåtna")]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Skriv in en epost")]
    [EmailAddress(ErrorMessage = "Fel format på eposten")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Skriv in ett lösenord")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Skriv in ett bekräftelselösenord")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Båda lösenorden måste matcha")]
    public string ConfirmPassword { get; set; }
}
