using DataAccess.Abstract;
using Entities.Abstract;
using System.Linq.Expressions;

namespace DataAccess.Concrete.InMemory;

public abstract class ImEntityRepositoryBase<TEntity> : IEntityRepositoryAsync<TEntity>
    where TEntity : class, IEntity, new()
{
    private List<TEntity> _;
    protected ImEntityRepositoryBase()
    {
        _ = new();
    }

    public abstract Task AddAsync(TEntity entity);
    public abstract Task DeleteAsync(TEntity entity);
    public abstract Task UpdateAsync(TEntity entity);


    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        return _.AsQueryable().Where(filter).SingleOrDefault();
    }

    public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        return filter == null ? _.ToList() : _.AsQueryable().Where(filter).ToList();
    }
}
