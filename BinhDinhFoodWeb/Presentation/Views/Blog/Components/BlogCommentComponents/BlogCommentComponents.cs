using BinhDinhFood.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Views.Blog.Components.BlogCommentComponents;

public class BlogCommentComponents : ViewComponent
{
    private readonly IBlogRepository _repo;
    public BlogCommentComponents(IBlogRepository repo)
    {
        _repo = repo;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        //@await Component.InvokeAsync("BlogCommentComponents")

        var obj = await _repo.GetListAsync();
        return View("BlogCommentComponents", obj);
    }
}
