using BinhDinhFood.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Views.Cart.Components.OrdersComponent;

public class OrdersComponent : ViewComponent
{
    public IViewComponentResult Invoke(IEnumerable<Order> obj)
    {
        return View(obj);
    }
}
