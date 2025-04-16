using Hattmakare.Models.Hats;

namespace Hattmakare.Models.Order;

public class OrderHatsViewModel
{
    public List<StandardHatViewModel> Hats { get; set; }
    public List<StandardHatViewModel> SpecialHats { get; set; } // Ändra till SpecialHatViewModel
}
