using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Views.Shared.Components.AccountComponent;

public class AccountComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
