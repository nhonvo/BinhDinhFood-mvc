using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Infrastructure;

namespace BinhDinhFood.Infrastructure.Repositories;

public class ProductRatingRepository : RepositoryBase<ProductRating>, IProductRatingRepository
{

    public ProductRatingRepository(BinhDinhFoodDbContext context) : base(context)
    {
    }
    //public async Task<ProductRating> GetProductRatingAsync(int id) 
    //    => await _context.ProductRatings.FindAsync(id);
    //public async Task<List<ProductRating>> GetAllProductRatingsAsync(int id) 
    //    => await _context.ProductRatings
    //        .Where(x => x.ProductId == id)
    //        .ToListAsync();

    //public async Task Save()=> await _context.SaveChangesAsync();

    //public async Task Add(ProductRating pd) => await _context.ProductRatings.AddAsync(pd);
}
