using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BinhDinhFood.Areas.Admin.Controllers;

[Area("Admin")]
public class AdmCategoryController : Controller
{
    private readonly BinhDinhFoodDbContext _context;

    public AdmCategoryController(BinhDinhFoodDbContext context)
    {
        _context = context;
    }

    // GET: Admin/AdmCategory
    public async Task<IActionResult> Index()
    {
        return _context.Categories != null ?
                    View(await _context.Categories.ToListAsync()) :
                    Problem("Entity set 'BinhDinhFoodDbContext.Categories'  is null.");
    }

    // GET: Admin/AdmCategory/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Categories == null)
        {
            return NotFound();
        }

        var category = await _context.Categories
            .FirstOrDefaultAsync(m => m.CategoryId == id);
        return category == null ? NotFound() : View(category);
    }

    // GET: Admin/AdmCategory/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/AdmCategory/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDateCreated")] Category category)
    {
        if (ModelState.IsValid)
        {
            category.CategoryDateCreated = DateTime.Now;
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    // GET: Admin/AdmCategory/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Categories == null)
        {
            return NotFound();
        }

        var category = await _context.Categories.FindAsync(id);
        return category == null ? NotFound() : View(category);
    }

    // POST: Admin/AdmCategory/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,CategoryDateCreated")] Category category)
    {
        if (id != category.CategoryId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                category.CategoryDateCreated = DateTime.Now;
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.CategoryId))
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
        return View(category);
    }

    // GET: Admin/AdmCategory/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Categories == null)
        {
            return NotFound();
        }

        var category = await _context.Categories
            .FirstOrDefaultAsync(m => m.CategoryId == id);
        return category == null ? NotFound() : View(category);
    }

    // POST: Admin/AdmCategory/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Categories == null)
        {
            return Problem("Entity set 'BinhDinhFoodDbContext.Categories'  is null.");
        }
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CategoryExists(int id)
    {
        return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
    }
}
