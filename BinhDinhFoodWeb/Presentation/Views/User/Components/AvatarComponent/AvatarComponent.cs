using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Views.User.Components.AvatarComponent;

public class AvatarComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
