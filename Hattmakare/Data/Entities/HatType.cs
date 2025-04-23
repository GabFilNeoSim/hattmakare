using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Data.Entities
{
    public class HatType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Hat> Hats { get; set; }
    }
}
