using BinhDinhFood.Domain.Entities;

namespace BinhDinhFood.Application.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    public Task UpdatePaymentState(int orderId);
}
