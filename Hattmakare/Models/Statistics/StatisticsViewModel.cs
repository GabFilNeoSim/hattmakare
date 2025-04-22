using Hattmakare.Models.Customer;
using Hattmakare.Models.Hats;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hattmakare.Models.Statistics
{
    public class StatisticsViewModel
    {
        public int? CustomerId { get; set; }
        public List<string> HatNames { get; set; }
        public List<int> Sales { get; set; }
        public List<HatViewModel> Hats { get; set; }
        public List<SelectListItem> Customers { get; set; }
    }
}
