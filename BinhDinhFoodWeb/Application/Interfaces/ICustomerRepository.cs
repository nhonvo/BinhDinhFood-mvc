using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Presentation.Models;
namespace BinhDinhFood.Application.Interfaces;

public interface ICustomerRepository : IRepository<Customer>
{
    //Lấy ra danh sách customer : số lượng đơn hàng từ startDate đến endDate
    public List<Table> GetTopOrder(DateTime startDate, DateTime endDate);

    //Lấy ra danh sách customer : số tiền đã tiêu từ startDate đến endDate
    public List<Table> GetTopRevenue(DateTime startDate, DateTime endDate);
}
