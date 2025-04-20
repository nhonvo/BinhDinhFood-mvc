using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Infrastructure;

namespace BinhDinhFood.Infrastructure.Repositories;

public class FavoriteRepository : RepositoryBase<Favorite>, IFavoriteRepository
{
    public FavoriteRepository(BinhDinhFoodDbContext context) : base(context)
    {

    }
}
