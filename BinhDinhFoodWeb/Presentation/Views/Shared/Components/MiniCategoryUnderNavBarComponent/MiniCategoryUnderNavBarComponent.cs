using BinhDinhFood.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Views.Shared.Components.MiniCategoryUnderNavBarComponent;

public class MiniCategoryUnderNavBarComponent : ViewComponent
{
    private readonly ICategoryRepository _repo;
    public MiniCategoryUnderNavBarComponent(ICategoryRepository repo)
    {
        _repo = repo;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var obj = await _repo.GetListAsync();
        return View(obj);

    }
}
