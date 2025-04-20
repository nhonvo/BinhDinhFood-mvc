using System.Linq.Expressions;

namespace BinhDinhFood.Application.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    void Delete(Expression<Func<TEntity, bool>> where);
    int Count(Expression<Func<TEntity, bool>> where);
    Task<IEnumerable<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        int skip = 0,
        int take = 0
    );
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
    Task<TEntity> GetByIdAsync(object id);
    Task SaveAsync();
}
