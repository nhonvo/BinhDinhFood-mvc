using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Infrastructure;

namespace BinhDinhFood.Infrastructure.Repositories;

public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
{

    public OrderDetailRepository(BinhDinhFoodDbContext context) : base(context)
    {

    }
}
