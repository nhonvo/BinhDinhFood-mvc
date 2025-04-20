using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
namespace BinhDinhFood.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository _repoProduct;
    private readonly ICategoryRepository _repoCategory;
    private readonly IProductRatingRepository _repoProductRating;

    public ProductController(IProductRepository repoProduct, ICategoryRepository repoCategory, IProductRatingRepository repoProductRating)
    {
        _repoProduct = repoProduct;
        _repoCategory = repoCategory;
        _repoProductRating = repoProductRating;
    }
    public async Task<IActionResult> Index(int page = 1)
    {
        var obj = await _repoProduct.GetListAsync();
        return View(obj.ToPagedList(page, 9));
    }
    // List view
    public async Task<IActionResult> ListView(int page = 1)
    {
        var obj = await _repoProduct.GetListAsync();
        return View(obj.ToPagedList(page, 5));
    }
    // Product Details page
    public async Task<IActionResult> ProductDetail(int id)
    {
        var obj = await _repoProduct.GetByIdAsync(id);
        ViewBag.Review = _repoProductRating.Count(x => x.ProductId == id);
        return View(obj);
    }
    // Write review for PD 
    public async Task<IActionResult> LeaveReview(int id)
    {
        var obj = await _repoProduct.GetByIdAsync(id);
        return View(obj);
    }
    [HttpPost]
    public async Task<IActionResult> LeaveReview(IFormCollection form, int id)
    {
        if (!User.Identity.IsAuthenticated || User.IsInRole("Admin"))
        {
            return RedirectToAction("Login", "User");
        }

        ProductRating pd = new ProductRating();
        pd.CustomerId = 1;
        pd.ProductId = id;
        pd.Stars = Convert.ToInt32(form["rating-input"]);
        pd.RatingContent = form["RatingContent"];
        pd.PRDateCreated = DateTime.Now;
        await _repoProductRating.AddAsync(pd);
        await _repoProductRating.SaveAsync();
        await _repoProduct.UpdateRating(id);
        return RedirectToAction("Confirm", "Product");
    }


    // Confirm feature
    public IActionResult Confirm()
    {
        return View();
    }
    // Search feature
    public async Task<IActionResult> SearchByFilter(string name, int? categoryId, string sort, int page = 1)
    {
        IEnumerable<Product> obj = await _repoProduct.GetListAsync();
        if (!string.IsNullOrEmpty(name))
        {
            obj = await _repoProduct.GetListAsync(filter: x => x.ProductName.Contains(name));
        }

        if (categoryId.HasValue)
        {
            obj = await _repoProduct.GetListAsync(filter: x => x.CategoryId == categoryId);
        }

        switch (sort)
        {
            case "date_desc":
                obj = await _repoProduct.GetListAsync(orderBy: x => x.OrderBy(x => x.ProductDateCreated));
                break;
            case "date_asce":
                obj = await _repoProduct.GetListAsync(orderBy: x => x.OrderByDescending(x => x.ProductDateCreated));
                break;
            case "price_desc":
                obj = await _repoProduct.GetListAsync(orderBy: x => x.OrderBy(x => x.ProductPrice));
                break;
            case "price_asce":
                obj = await _repoProduct.GetListAsync(orderBy: x => x.OrderByDescending(x => x.ProductPrice));
                break;
            case "discount":
                obj = await _repoProduct.GetListAsync(orderBy: x => x.OrderByDescending(x => x.ProductDiscount));
                break;
        }
        return View(obj.ToPagedList(page, 9));
    }
    public async Task<IActionResult> Filter()
    {
        var objCategory = await _repoCategory.GetListAsync();
        ViewData["ListCategory"] = objCategory;
        //@foreach(var item in ViewData["ListCategory"] as List<Category>)
        //{
        //    < li >
        //        < label class="container_check">
        //            @item.CategoryName
        //            <input type="checkbox" value="@item.CategoryId = @ViewData["Test"]" id="item_@item.CategoryId">
        //            <span class="checkmark"></span>
        //        </label>
        //    </li>
        //}
        return View();
    }
}
