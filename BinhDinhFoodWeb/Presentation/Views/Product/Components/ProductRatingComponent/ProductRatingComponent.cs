using BinhDinhFood.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Views.Product.Components.ProductRatingComponent;

public class ProductRatingComponent : ViewComponent
{
    private readonly IProductRatingRepository _repo;
    public ProductRatingComponent(IProductRatingRepository repo)
    {
        _repo = repo;
    }
    public async Task<IViewComponentResult> InvokeAsync(int id)
    {
        var obj = await _repo.GetListAsync(filter: x => x.ProductId == id, includeProperties: "Customer");
        return View("ProductRatingComponent", obj);
    }
}
