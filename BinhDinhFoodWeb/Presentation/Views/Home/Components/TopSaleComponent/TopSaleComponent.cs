using BinhDinhFood.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Views.Home.Components.TopSaleComponent;

public class TopSaleComponent : ViewComponent
{
    private readonly IProductRepository _repo;

    public TopSaleComponent(IProductRepository repo)
    {
        _repo = repo;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var obj = await _repo.GetListAsync(filter: x => x.ProductDiscount > 0, orderBy: x => x.OrderByDescending(x => x.ProductId), take: 8);
        return View("TopSaleComponent", obj);
    }
}
