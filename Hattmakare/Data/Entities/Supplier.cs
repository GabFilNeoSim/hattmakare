using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Data.Entities
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
