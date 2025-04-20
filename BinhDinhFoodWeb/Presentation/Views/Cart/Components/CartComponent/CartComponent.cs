using BinhDinhFood.Presentation.Models.Report;
using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Views.Cart.Components.CartComponent;

public class CartComponent : ViewComponent
{
    public IViewComponentResult Invoke(IEnumerable<Item> list)
    {
        return View(list);
    }
}
