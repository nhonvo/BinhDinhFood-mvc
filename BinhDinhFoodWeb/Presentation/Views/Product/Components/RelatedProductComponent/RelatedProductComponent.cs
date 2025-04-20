using BinhDinhFood.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Views.Product.Components.RelatedProductComponent;

public class RelatedProductComponent : ViewComponent
{
    private readonly IProductRepository _repo;
    public RelatedProductComponent(IProductRepository repo)
    {
        _repo = repo;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var obj = await _repo.GetListAsync();
        return View("RelatedProductComponent", obj);
    }
}
