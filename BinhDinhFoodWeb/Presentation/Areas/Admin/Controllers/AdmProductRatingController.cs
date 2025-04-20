using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BinhDinhFood.Areas.Admin.Controllers;

[Area("Admin")]
public class AdmProductRatingController : Controller
{
    private readonly BinhDinhFoodDbContext _context;

    public AdmProductRatingController(BinhDinhFoodDbContext context)
    {
        _context = context;
    }

    // GET: Admin/AdmProductRating
    public async Task<IActionResult> Index()
    {
        var binhDinhFoodDbContext = _context.ProductRatings.Include(p => p.Customer).Include(p => p.Product);
        return View(await binhDinhFoodDbContext.ToListAsync());
    }

    // GET: Admin/AdmProductRating/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.ProductRatings == null)
        {
            return NotFound();
        }

        var productRating = await _context.ProductRatings
            .Include(p => p.Customer)
            .Include(p => p.Product)
            .FirstOrDefaultAsync(m => m.ProductRatingId == id);
        return productRating == null ? NotFound() : View(productRating);
    }

    // GET: Admin/AdmProductRating/Create
    public IActionResult Create()
    {
        ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerPassword");
        ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
        return View();
    }

    // POST: Admin/AdmProductRating/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProductRatingId,Stars,RatingContent,PRDateCreated,ProductId,CustomerId")] ProductRating productRating)
    {
        if (ModelState.IsValid)
        {
            _context.Add(productRating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerPassword", productRating.CustomerId);
        ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", productRating.ProductId);
        return View(productRating);
    }

    // GET: Admin/AdmProductRating/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.ProductRatings == null)
        {
            return NotFound();
        }

        var productRating = await _context.ProductRatings.FindAsync(id);
        if (productRating == null)
        {
            return NotFound();
        }
        ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerPassword", productRating.CustomerId);
        ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", productRating.ProductId);
        return View(productRating);
    }

    // POST: Admin/AdmProductRating/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProductRatingId,Stars,RatingContent,PRDateCreated,ProductId,CustomerId")] ProductRating productRating)
    {
        if (id != productRating.ProductRatingId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(productRating);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductRatingExists(productRating.ProductRatingId))
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
        ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerPassword", productRating.CustomerId);
        ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", productRating.ProductId);
        return View(productRating);
    }

    // GET: Admin/AdmProductRating/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.ProductRatings == null)
        {
            return NotFound();
        }

        var productRating = await _context.ProductRatings
            .Include(p => p.Customer)
            .Include(p => p.Product)
            .FirstOrDefaultAsync(m => m.ProductRatingId == id);
        return productRating == null ? NotFound() : View(productRating);
    }

    // POST: Admin/AdmProductRating/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.ProductRatings == null)
        {
            return Problem("Entity set 'BinhDinhFoodDbContext.ProductRatings'  is null.");
        }
        var productRating = await _context.ProductRatings.FindAsync(id);
        if (productRating != null)
        {
            _context.ProductRatings.Remove(productRating);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductRatingExists(int id)
    {
        return (_context.ProductRatings?.Any(e => e.ProductRatingId == id)).GetValueOrDefault();
    }
}
