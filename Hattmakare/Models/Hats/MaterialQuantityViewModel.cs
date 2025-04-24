namespace Hattmakare.Models.Hats
{
    public class MaterialQuantityViewModel
    {
        public int MaterialId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
