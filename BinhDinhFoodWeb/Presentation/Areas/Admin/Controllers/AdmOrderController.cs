using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BinhDinhFood.Areas.Admin.Controllers;

[Area("Admin")]
public class AdmOrderController : Controller
{
    private readonly BinhDinhFoodDbContext _context;

    public AdmOrderController(BinhDinhFoodDbContext context)
    {
        _context = context;
    }

    // GET: Admin/AdmOrder
    public async Task<IActionResult> Index()
    {
        var binhDinhFoodDbContext = _context.Orders.Include(o => o.Customer);
        return View(await binhDinhFoodDbContext.ToListAsync());
    }

    // GET: Admin/AdmOrder/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Orders == null)
        {
            return NotFound();
        }

        var order = await _context.Orders
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(m => m.OrderId == id);
        return order == null ? NotFound() : View(order);
    }

    // GET: Admin/AdmOrder/Create
    public IActionResult Create()
    {
        ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerPassword");
        return View();
    }

    // POST: Admin/AdmOrder/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("OrderId,DayOrder,DayDelivery,PaidState,DeliveryState,TotalMoney,CustomerId")] Order order)
    {
        if (ModelState.IsValid)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerPassword", order.CustomerId);
        return View(order);
    }

    // GET: Admin/AdmOrder/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Orders == null)
        {
            return NotFound();
        }

        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerPassword", order.CustomerId);
        return View(order);
    }

    // POST: Admin/AdmOrder/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("OrderId,DayOrder,DayDelivery,PaidState,DeliveryState,TotalMoney,CustomerId")] Order order)
    {
        if (id != order.OrderId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(order);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.OrderId))
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
        ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerPassword", order.CustomerId);
        return View(order);
    }

    // GET: Admin/AdmOrder/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Orders == null)
        {
            return NotFound();
        }

        var order = await _context.Orders
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(m => m.OrderId == id);
        return order == null ? NotFound() : View(order);
    }

    // POST: Admin/AdmOrder/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Orders == null)
        {
            return Problem("Entity set 'BinhDinhFoodDbContext.Orders'  is null.");
        }
        var order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool OrderExists(int id)
    {
        return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
    }
}
