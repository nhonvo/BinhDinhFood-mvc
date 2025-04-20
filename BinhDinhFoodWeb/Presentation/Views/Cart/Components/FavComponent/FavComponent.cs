using BinhDinhFood.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Views.Cart.Components.FavComponent;

public class FavComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(IEnumerable<Favorite> fav)
    {
        return View("FavComponent", fav);
    }
}
