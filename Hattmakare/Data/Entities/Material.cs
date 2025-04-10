using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hattmakare.Data.Entities
{
    public class Material
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Unit { get; set; }

        [Required]
        public int PurchasePrice { get; set; }

        // Foreign keys
        public int SupplierId { get; set; }

        // Navigation props
        [ForeignKey(nameof(SupplierId))]
        public virtual Supplier Supplier { get; set; }

    }
}
