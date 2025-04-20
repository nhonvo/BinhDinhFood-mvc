using BinhDinhFood.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Views.Home.Components.NewsComponent;

public class NewsComponent : ViewComponent
{
    private readonly IBlogRepository _repo;
    public NewsComponent(IBlogRepository repo)
    {
        _repo = repo;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var obj = await _repo.GetListAsync(take: 4);
        return View("NewsComponent", obj);
    }
}
