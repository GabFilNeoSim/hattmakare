﻿using System.ComponentModel.DataAnnotations;

namespace Hattmakare.Models.Material;

public class AddMaterialViewModel
{
    [Required(ErrorMessage = "Skriv in ett namn")]
    [StringLength(50)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Skriv in en enhet")]
    [StringLength(20)]
    public string Unit { get; set; }

    [Required(ErrorMessage = "Skriv in ett pris")]
    [DataType(DataType.Currency)]
    [Range(0d, Double.MaxValue, ErrorMessage = "Priset måste vara 0 eller större")]
    public decimal Price { get; set; } = 0m;

    [Required(ErrorMessage = "Skriv in en leverantör")]
    [StringLength(50)]
    public string Supplier { get; set; }

    public bool IsDecoration { get; set; } = false;
}
