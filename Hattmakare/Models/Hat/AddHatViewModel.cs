using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Models.Hat;

public class AddHatViewModel
{
    [Required(ErrorMessage = "Please enter a name")]
    [MaxLength(100)]
    public string Name { get; set; }

    //[Required(ErrorMessage = "Please enter a price")]
    //[Range(0, double.MaxValue, ErrorMessage = "The price must be 0 or higher")]
    //public decimal Price {  get; set; }

    [Required(ErrorMessage = "Please upload an image")]
    public IFormFile Image { get; set; }
}
