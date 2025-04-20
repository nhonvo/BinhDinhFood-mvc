using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Presentation.Models;
namespace BinhDinhFood.Application.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    //Lấy ra danh sách Product : số lượng bán được từ startDate đến endDate
    public Table[] GetProductWithAmount(DateTime startDate, DateTime endDate);

    //Lấy ra danh sách Product : doanh thu từ startDate đến endDate
    public Table[] GetProductWithRevenue(DateTime startDate, DateTime endDate);

    //Lấy ra danh sách Tháng : số lượng bán được của sản phẩm có id là productId
    public Table[] GetProductAmountInYear(int productId);
    public Table[] GetProductRevenueInYear(int productId);
    public List<Table> GetListProduct();
    public Task UpdateRating(int id);
    public Task UpdateAmount(int id);
}
