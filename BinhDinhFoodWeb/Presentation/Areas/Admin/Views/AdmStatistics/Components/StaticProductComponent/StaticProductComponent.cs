using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Areas.Admin.Views.AdmStatistics.Components.StaticProductComponent;

public class StaticProductComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
