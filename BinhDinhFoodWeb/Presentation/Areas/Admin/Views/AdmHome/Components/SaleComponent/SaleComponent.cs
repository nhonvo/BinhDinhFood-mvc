using BinhDinhFood.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Areas.Admin.Views.AdmHome.Components.SaleComponent;

public class SaleComponent : ViewComponent
{
    private readonly BinhDinhFoodDbContext _context;

    public SaleComponent(BinhDinhFoodDbContext context)
    {
        _context = context;
    }
    public IViewComponentResult Invoke(string saleOrder)
    {
        if (saleOrder == null)
        {
            saleOrder = "today";
        }

        int objSale = 0;
        int objLastSale = 0;
        switch (saleOrder)
        {
            case "today":
                var lastDate = DateTime.Now.Date.AddDays(-1);
                objSale = _context.Orders.Where(
                    x => x.DayOrder.Date == DateTime.Now.Date
                    ).Count();
                objLastSale = _context.Orders.Where(
                    x => x.DayOrder.Date == lastDate
                    ).Count();

                ViewBag.SaleDay = "Hôm nay";
                break;
            case "thisMonth":
                var lastMonthDate = DateTime.Now.Date.AddMonths(-1);
                objSale = _context.Orders.Where(
                    x => x.DayOrder.Month == DateTime.Now.Month
                    && x.DayOrder.Year == DateTime.Now.Year
                    ).Count();
                objLastSale = _context.Orders.Where(
                    x => x.DayOrder.Year == lastMonthDate.Year
                    && x.DayOrder.Month == lastMonthDate.Month
                    ).Count();
                ViewBag.SaleDay = "Tháng này";
                break;
            case "thisYear":
                var lastYearDate = DateTime.Now.Date.AddYears(-1);
                objSale = _context.Orders.Where(x => x.DayOrder.Year == DateTime.Now.Year).Count();
                objLastSale = _context.Orders.Where(x => x.DayOrder.Year == lastYearDate.Year).Count();
                ViewBag.SaleDay = "Năm này";
                break;
        }
        ViewBag.SalePercent = objLastSale > 0 ? (objSale - objLastSale) * 100 / objLastSale : objSale * 100;
        ViewBag.SaleColor = objSale == objLastSale ? "text-muted" : objSale > objLastSale ? "text-success" : "text-danger";
        ViewBag.SaleStatus = objSale == objLastSale ? "" : objSale > objLastSale ? "tăng" : "giảm";
        ViewBag.SaleNumbers = objSale;
        return View();
    }
}
