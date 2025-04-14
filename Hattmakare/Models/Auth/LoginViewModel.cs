using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Models.Auth;

public class LoginViewModel
{
    [Required(ErrorMessage = "Skriva in en epost")]
    [EmailAddress(ErrorMessage = "Fel format på eposten")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Skriv in ett lösenord")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool RememberMe { get; set; } = false;
}
