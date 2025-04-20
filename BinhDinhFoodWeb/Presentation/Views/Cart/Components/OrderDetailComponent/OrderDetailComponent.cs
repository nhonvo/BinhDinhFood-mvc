using BinhDinhFood.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Views.Cart.Components.OrderDetailComponent;

public class OrderDetailComponent : ViewComponent
{
    public IViewComponentResult Invoke(IEnumerable<OrderDetail> obj)
    {
        return View(obj);
    }
}
