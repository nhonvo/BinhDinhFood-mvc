using BinhDinhFood.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BinhDinhFood.Controllers;

public class BlogController(IBlogRepository repo) : Controller
{
    private readonly IBlogRepository _repo = repo;

    public async Task<IActionResult> Index(int page = 1)
    {
        var listBlog = await _repo.GetListAsync(orderBy: x => x.OrderBy(x => x.BlogId));
        return View(listBlog.ToPagedList(page, 4));
    }
    public async Task<IActionResult> BlogPost(int id)
    {
        var post = await _repo.GetByIdAsync(id);
        return View(post);
    }

}
