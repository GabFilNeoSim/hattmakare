using Hattmakare.Models.Customer;
using Hattmakare.Models.Hats;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hattmakare.Models.Statistics
{
    public class StatisticsViewModel
    {
        public string? CustomerId { get; set; }
        public string? HatId { get; set; }
        public List<string> HatNames { get; set; }
        public List<int> Sales { get; set; }
        
        public List<string> DailyLabels { get; set; } 
        public List<int> DailySales { get; set; }
        public List<int> QuarterlySales { get; set; }
        public List<int> MonthlySales { get; set; }


        public List<SelectListItem> Hats { get; set; }
        public List<SelectListItem> Customers { get; set; }
        public List<HatsViewModel> hatsViewModels { get; set; }
    }
}
