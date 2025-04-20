using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Views.User.Components.ShowProfileComponent;

public class ShowProfileComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
