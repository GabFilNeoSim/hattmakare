namespace Hattmakare.Models.Material;

public class EditMaterialViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public decimal Price { get; set; }
    public string Supplier { get; set; }
    public bool IsDecoration { get; set; }
}
