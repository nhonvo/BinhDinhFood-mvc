using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Views.User.Components.EditProfileComponent;

public class EditProfileComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
