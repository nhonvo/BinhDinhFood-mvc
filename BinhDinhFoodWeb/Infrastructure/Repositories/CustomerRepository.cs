using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Infrastructure;
using BinhDinhFood.Presentation.Models;

namespace BinhDinhFood.Infrastructure.Repositories;

public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    public CustomerRepository(BinhDinhFoodDbContext context) : base(context)
    {

    }
    public List<Table> GetTopOrder(DateTime startDate, DateTime endDate)
    {
        var toporders = _context.Orders
                                .Where(y => y.DayOrder >= startDate && y.DayOrder < endDate)
                                .GroupBy(y => new { y.Customer.CustomerId, y.Customer.CustomerFullName })
                                .Select(t => new Table
                                {
                                    Key = t.Key.CustomerFullName,
                                    Value = t.Count()
                                }).ToList();

        return toporders;
    }

    public List<Table> GetTopRevenue(DateTime startDate, DateTime endDate)
    {
        var toprevenue = _context.Orders
                                .Where(y => y.DayOrder >= startDate && y.DayOrder < endDate)
                                .GroupBy(y => new { y.Customer.CustomerId, y.Customer.CustomerFullName })
                                .Select(t => new Table
                                {
                                    Key = t.Key.CustomerFullName,
                                    Value = (int)t.Sum(k => k.TotalMoney)
                                }).ToList();


        return toprevenue;
    }

}
