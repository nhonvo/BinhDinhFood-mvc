using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Infrastructure;

namespace BinhDinhFood.Infrastructure.Repositories;

public class BannerRepository : RepositoryBase<Banner>, IBannerRepository
{
    public BannerRepository(BinhDinhFoodDbContext context) : base(context)
    {

    }
}
