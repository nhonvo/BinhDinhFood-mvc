using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BinhDinhFood.Areas.Admin.Controllers;

[Area("Admin")]
public class AdmBlogController : Controller
{
    private readonly IWebHostEnvironment _appEnvironment;
    private readonly BinhDinhFoodDbContext _context;

    public AdmBlogController(BinhDinhFoodDbContext context, IWebHostEnvironment appEnvironment)
    {
        _context = context;
        _appEnvironment = appEnvironment;
    }

    // GET: Admin/AdmBlog
    public async Task<IActionResult> Index()
    {
        return _context.Blog != null ?
                    View(await _context.Blog.ToListAsync()) :
                    Problem("Entity set 'BinhDinhFoodDbContext.Blog'  is null.");
    }

    // GET: Admin/AdmBlog/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Blog == null)
        {
            return NotFound();
        }

        var blog = await _context.Blog
            .FirstOrDefaultAsync(m => m.BlogId == id);
        return blog == null ? NotFound() : View(blog);
    }

    // GET: Admin/AdmBlog/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/AdmBlog/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("BlogId,BlogName,BlogContent,BlogImage,BlogDateCreated")] Blog blog)
    {
        IFormFileCollection files = HttpContext.Request.Form.Files;

        if (ModelState.IsValid)
        {
            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {
                    var file = Image;
                    var uploads = Path.Combine(_appEnvironment.WebRootPath, "Content\\img\\products\\");
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            blog.BlogImage = fileName;
                        }
                    }
                }
            }
            blog.BlogDateCreated = DateTime.Now;
            _context.Add(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(blog);
    }

    // GET: Admin/AdmBlog/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Blog == null)
        {
            return NotFound();
        }

        var blog = await _context.Blog.FindAsync(id);
        return blog == null ? NotFound() : View(blog);
    }

    // POST: Admin/AdmBlog/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("BlogId,BlogName,BlogContent,BlogImage,BlogDateCreated")] Blog blog)
    {
        IFormFileCollection files = HttpContext.Request.Form.Files;

        if (id != blog.BlogId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        var uploads = Path.Combine(_appEnvironment.WebRootPath, "Content\\img\\products\\");
                        if (file.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                blog.BlogImage = fileName;
                            }
                        }
                    }
                }
                blog.BlogDateCreated = DateTime.Now;
                _context.Update(blog);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(blog.BlogId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(blog);
    }

    // GET: Admin/AdmBlog/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Blog == null)
        {
            return NotFound();
        }

        var blog = await _context.Blog
            .FirstOrDefaultAsync(m => m.BlogId == id);
        return blog == null ? NotFound() : View(blog);
    }

    // POST: Admin/AdmBlog/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Blog == null)
        {
            return Problem("Entity set 'BinhDinhFoodDbContext.Blog'  is null.");
        }
        var blog = await _context.Blog.FindAsync(id);
        if (blog != null)
        {
            _context.Blog.Remove(blog);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BlogExists(int id)
    {
        return (_context.Blog?.Any(e => e.BlogId == id)).GetValueOrDefault();
    }
}
