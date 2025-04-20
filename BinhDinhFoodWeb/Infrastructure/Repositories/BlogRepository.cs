using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Infrastructure;

namespace BinhDinhFood.Infrastructure.Repositories;

public class BlogRepository : RepositoryBase<Blog>, IBlogRepository
{
    public BlogRepository(BinhDinhFoodDbContext context) : base(context) { }
}
