using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Presentation.Models;

namespace BinhDinhFood.Application.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    //public Task<List<Category>> Get();
    //public Task<Category> GetById(int id);
    public Table[] GetRevenueStructure(int year);
}
