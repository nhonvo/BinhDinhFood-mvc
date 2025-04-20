using System.Security.Claims;
using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BinhDinhFood.Areas.Admin.Controllers;

[Area("Admin")]
public class AdmProductController : Controller
{
    private readonly BinhDinhFoodDbContext _context;
    private readonly IWebHostEnvironment _appEnvironment;

    public AdmProductController(BinhDinhFoodDbContext context, IWebHostEnvironment appEnvironment)
    {
        _context = context;
        _appEnvironment = appEnvironment;
    }

    // GET: Admin/AdmProduct
    public async Task<IActionResult> Index(int page = 1)
    {
        var binhDinhFoodDbContext = _context.Products.Include(p => p.Category);
        var obj = await binhDinhFoodDbContext.ToListAsync();
        return View(obj.ToPagedList(page, 10));
    }

    // GET: Admin/AdmProduct/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Products == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.ProductId == id);
        return product == null ? NotFound() : View(product);
    }

    // GET: Admin/AdmProduct/Create
    public IActionResult Create()
    {
        if (!User.Identity.IsAuthenticated && User.FindFirstValue(ClaimTypes.Role) != "Admin")
        {
            return RedirectToAction("Login", "AdmAccount");
        }

        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
        return View();
    }

    // POST: Admin/AdmProduct/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductPrice,ProductDescription,ProductAmount,ProductDiscount,ProductImage,ProductDateCreated,CategoryId")] Product product)
    {
        if (ModelState.IsValid)
        {
            IFormFileCollection files = HttpContext.Request.Form.Files;
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
                            product.ProductImage = fileName;
                        }
                    }
                }
            }
            product.ProductDateCreated = DateTime.Now;
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
        return View(product);

    }

    // GET: Admin/AdmProduct/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {

        if (id == null || _context.Products == null)
        {
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
        return View(product);
    }

    // POST: Admin/AdmProduct/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductPrice,ProductDescription,ProductAmount,ProductDiscount,ProductImage,ProductDateCreated,CategoryId")] Product product)
    {
        IFormFileCollection files = HttpContext.Request.Form.Files;

        if (id != product.ProductId)
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
                                product.ProductImage = fileName;
                            }
                        }
                    }
                }
                product.ProductDateCreated = DateTime.Now;
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
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
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
        return View(product);
    }

    // GET: Admin/AdmProduct/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Products == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.ProductId == id);
        return product == null ? NotFound() : View(product);
    }

    // POST: Admin/AdmProduct/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Products == null)
        {
            return Problem("Entity set 'BinhDinhFoodDbContext.Products'  is null.");
        }
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id)
    {
        return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
    }
}
