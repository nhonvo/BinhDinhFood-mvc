namespace BinhDinhFood.Presentation.Models;

public class Period
{
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public Period(int option = 0)
    {
        startDate = DateTime.Now.Date;
        switch (option)
        {
            case 1:
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 1);
                endDate = startDate.AddMonths(1).AddSeconds(-1);
                break;
            case 2:
                startDate = new DateTime(DateTime.Now.Year, 1, 1, 1, 0, 0, 1);
                endDate = startDate.AddYears(1).AddSeconds(-1);
                break;
            case 3:
                startDate = new DateTime(DateTime.Now.Year - 100, 1, 1, 0, 0, 0);
                endDate = startDate.AddYears(101).AddSeconds(-1);
                break;
            default:
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 1);
                endDate = startDate.AddDays(1).AddSeconds(-1);
                break;
        }
    }
}
