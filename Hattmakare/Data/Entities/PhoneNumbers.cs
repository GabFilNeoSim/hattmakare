using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Data.Entities
{
    public class PhoneNumbers
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
    }
}
