namespace Hattmakare.Models.Home;

public class CalendarPopupViewModel
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
