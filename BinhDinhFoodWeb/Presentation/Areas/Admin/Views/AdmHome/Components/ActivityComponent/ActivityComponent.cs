using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BinhDinhFood.Areas.Admin.Views.AdmHome.Components.ActivityComponent;

public class ActivityComponent : ViewComponent
{
    private readonly BinhDinhFoodDbContext _context;

    public ActivityComponent(BinhDinhFoodDbContext context)
    {
        _context = context;
    }
    public async Task<IViewComponentResult> InvokeAsync(string order)
    {
        var color = new List<string> {
            "text-danger",
            "text-primary",
            "text-secondary",
            "text-info",
            "text-muted",
            "text-warning",
            "text-success"
        };
        var random = new Random();
        IQueryable<OrderDetail> query = _context.OrderDetails;
        switch (order)
        {
            case "today":
                //query = query.Where(od => od.Order.DayOrder.Date == DateTime.Now.Date);
                query = query.Where(od => od.Order.DayOrder.Date == DateTime.Now.Date);
                ViewBag.ActivityTime = "Hôm nay";
                break;
            case "month":
                query = query.Where(od =>
                od.Order.DayOrder.Month == DateTime.Now.Month &&
                od.Order.DayOrder.Year == DateTime.Now.Year);

                ViewBag.ActivityTime = "Tháng này";
                break;
            case "year":
                query = query.Where(od => od.Order.DayOrder.Year == DateTime.Now.Year);
                ViewBag.ActivityTime = "Năm nay";
                break;
            default:
                ViewBag.ActivityTime = "";
                break;
        }
        var result = await query
        .OrderByDescending(od => od.Order.DayDelivery)
        .ThenByDescending(od => od.Order.DayOrder)
        .Select(od => new ActivityViewModel()
        {
            BuyTime = od.Order.DayOrder,
            UserFullName = od.Order.Customer.CustomerFullName,
            Quantity = od.Quantity,
            ProductName = od.Product.ProductName,
            TextColor = color[random.Next(0, color.Count)]
        }).Take(7).ToListAsync();

        return View(result);
    }
}
