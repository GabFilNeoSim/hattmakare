using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hattmakare.Data.Entities
{
    public class StandardHat : Hat
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int? Size { get; set; }
        public int? Quantity {  get; set; }
    }
}
