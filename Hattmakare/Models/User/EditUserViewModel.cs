using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Models.User;

public class EditUserViewModel
{
    public string Id { get; set; }

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

    public bool IsAdmin { get; set; } = false;
}
