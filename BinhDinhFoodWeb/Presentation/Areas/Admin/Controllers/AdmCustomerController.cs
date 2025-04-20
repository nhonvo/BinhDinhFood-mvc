using BinhDinhFood.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BinhDinhFood.Areas.Admin.Controllers;

[Area("Admin")]
public class AdmCustomerController : Controller
{
    private readonly BinhDinhFoodDbContext _context;

    public AdmCustomerController(BinhDinhFoodDbContext context)
    {
        _context = context;
    }

    // GET: Admin/AdmCustomer
    public async Task<IActionResult> Index()
    {
        return _context.Customers != null ?
                    View(await _context.Customers.ToListAsync()) :
                    Problem("Entity set 'BinhDinhFoodDbContext.Customers'  is null.");
    }

    // GET: Admin/AdmCustomer/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Customers == null)
        {
            return NotFound();
        }

        var customer = await _context.Customers
            .FirstOrDefaultAsync(m => m.CustomerId == id);
        return customer == null ? NotFound() : View(customer);
    }

    //// GET: Admin/AdmCustomer/Create
    //public IActionResult Create()
    //{
    //    return View();
    //}

    //// POST: Admin/AdmCustomer/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("CustomerId,CustomerFullName,CustomerUserName,CustomerPassword,CustomerDateCreated,CustomerEmail,CustomerAddress,CustomerState,CustomerImage")] Customer customer)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _context.Add(customer);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(customer);
    //}

    //// GET: Admin/AdmCustomer/Edit/5
    //public async Task<IActionResult> Edit(int? id)
    //{
    //    if (id == null || _context.Customers == null)
    //    {
    //        return NotFound();
    //    }

    //    var customer = await _context.Customers.FindAsync(id);
    //    if (customer == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(customer);
    //}

    //// POST: Admin/AdmCustomer/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CustomerFullName,CustomerUserName,CustomerPassword,CustomerDateCreated,CustomerEmail,CustomerAddress,CustomerState,CustomerImage")] Customer customer)
    //{
    //    if (id != customer.CustomerId)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(customer);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!CustomerExists(customer.CustomerId))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(customer);
    //}

    // GET: Admin/AdmCustomer/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Customers == null)
        {
            return NotFound();
        }

        var customer = await _context.Customers
            .FirstOrDefaultAsync(m => m.CustomerId == id);
        return customer == null ? NotFound() : View(customer);
    }

    // POST: Admin/AdmCustomer/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Customers == null)
        {
            return Problem("Entity set 'BinhDinhFoodDbContext.Customers'  is null.");
        }
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CustomerExists(int id)
    {
        return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
    }
}
