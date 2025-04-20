using BinhDinhFood.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Views.Shared.Components.HeaderCategoryComponent;

public class HeaderCategoryComponent : ViewComponent
{
    private readonly ICategoryRepository _repoCategory;
    private readonly IProductRepository _productRepository;

    public HeaderCategoryComponent(ICategoryRepository repoCategory, IProductRepository productRepository)
    {
        _repoCategory = repoCategory;
        _productRepository = productRepository;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var obj = await _repoCategory.GetListAsync();
        //var obj = await _productRepository.GetListAsync();
        return View(obj);
    }
}
