using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Infrastructure;
using BinhDinhFood.Presentation.Models;
using Microsoft.EntityFrameworkCore;

namespace BinhDinhFood.Infrastructure.Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{

    public ProductRepository(BinhDinhFoodDbContext context) : base(context)
    {
    }
    // get list all product
    public List<Table> GetListProduct()
    {
        return _context.Products.Select(x => new Table
        {
            Key = x.ProductName,
            Value = x.ProductId
        }).ToList();
    }

    public Table[] GetProductAmountInYear(int productId)
    {
        Table[] data = new Table[13];
        for (int month = 1; month <= 12; month++)
        {
            //data[month] 
            var product = _context.Products.Include(x => x.OrderDetails).ThenInclude(y => y.Order).FirstOrDefault(x => x.ProductId == productId);

            if (product != null)
            {
                var dataInMonth = product.OrderDetails
                .Where(x => x.Order.DayOrder.Month == month && x.Order.DayOrder.Year == DateTime.Now.Year);
                data[month] = dataInMonth.Count() != 0
                    ? dataInMonth
                    .Select(od => new Table
                    {
                        Key = "Tháng " + month,
                        Value = od.Quantity
                    }).FirstOrDefault()
                    : new Table
                    {
                        Key = "Tháng " + month,
                        Value = 0
                    };
            }
            else
            {
                data[month] = new Table
                {
                    Key = "Tháng " + month,
                    Value = 0
                };
            }
        }
        return data;
    }
    public Table[] GetProductRevenueInYear(int productId)
    {
        Table[] data = new Table[13];
        for (int month = 1; month <= 12; month++)
        {
            var product = _context.Products.Include(x => x.OrderDetails).ThenInclude(y => y.Order).FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                var dataInMonth = product.OrderDetails
                                         .Where(x => x.Order.DayOrder.Month == month && x.Order.DayOrder.Year == DateTime.Now.Year);
                data[month] = dataInMonth.Count() != 0
                    ? dataInMonth
                .Select(od => new Table
                {
                    Key = "Tháng " + month,
                    Value = (int)(od.Quantity * od.UnitPrice)
                }).FirstOrDefault()
                    : new Table
                    {
                        Key = "Tháng " + month,
                        Value = 0
                    };
            }
            else
            {
                data[month] = new Table
                {
                    Key = "Tháng " + month,
                    Value = 0
                };
            }
        }
        return data;
    }
    public Table[] GetProductWithAmount(DateTime startDate, DateTime endDate)
    {
        var product = _context.OrderDetails
                              .Where(x => x.Order.DayOrder >= startDate && x.Order.DayOrder < endDate)
                              .GroupBy(x => new { x.Product.ProductId, x.Product.ProductName })
                              .Select(t => new Table
                              {
                                  Key = t.Key.ProductName,
                                  Value = t.Sum(k => k.Quantity)
                              })
                              .ToArray();
        //.Join(_context.OrderDetails, p => p.ProductId, d => d.ProductId, (p, d) => new { p, d })
        //.Join(_context.Orders, g => g.d.OrderId, o => o.OrderId, (g, o) => new { g, o })
        //.Where(y => y.o.DayOrder >= startDate && y.o.DayOrder < endDate)
        //.GroupBy(x => new { x.g.p.ProductId, x.g.p.ProductName })
        //.Select(t => new Table
        //{
        //    Key = t.Key.ProductName,
        //    Value = t.Sum(x => x.g.d.Quantity),
        //}).ToArray();
        return product;
    }

    public Table[] GetProductWithRevenue(DateTime startDate, DateTime endDate)
    {
        var product = _context.OrderDetails
                              .Where(x => x.Order.DayOrder >= startDate && x.Order.DayOrder < endDate)
                              .GroupBy(x => new { x.Product.ProductId, x.Product.ProductName })
                              .Select(t => new Table
                              {
                                  Key = t.Key.ProductName,
                                  Value = (int)t.Sum(k => k.Quantity * k.UnitPrice)
                              })
                              .ToArray();

        //.Join(_context.OrderDetails, p => p.ProductId, d => d.ProductId, (p, d) => new { p, d })
        //.Join(_context.Orders, g => g.d.OrderId, o => o.OrderId, (g, o) => new { g, o })
        //.Where(y => y.o.DayOrder >= startDate && y.o.DayOrder < endDate)
        //.GroupBy(x => new { x.g.p.ProductId, x.g.p.ProductName })
        //.Select(t => new Table
        //{
        //    Key = t.Key.ProductName,
        //    Value = Convert.ToInt32(t.Sum(k => k.g.d.Quantity * k.g.d.UnitPrice)),
        //}).ToArray();
        return product;
    }

    public async Task UpdateAmount(int id)
    {
        Product product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
        product.ProductAmount = product.ProductAmount - 1;
        _context.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRating(int id)
    {
        int countStar = (int)_context.ProductRatings.Where(x => x.ProductId == id).Select(x => x.Stars).Average();
        Product product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
        product.ProductRating = countStar;
        _context.Update(product);
        await _context.SaveChangesAsync();

    }
}
