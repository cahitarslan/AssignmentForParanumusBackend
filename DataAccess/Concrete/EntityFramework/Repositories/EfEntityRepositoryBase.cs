using DataAccess.Abstract;
using Entities.Abstract;
using Entities.Concrete.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework.Repositories;

public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepositoryAsync<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : IdentityDbContext<AppUser, AppRole, int>
{
    protected TContext Context { get; }
    public EfEntityRepositoryBase(TContext context)
    {
        Context = context;
    }

    public async Task AddAsync(TEntity entity)
    {
        var addedEntity = Context.Entry(entity);
        addedEntity.State = EntityState.Added;
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        var deletedEntity = Context.Entry(entity);
        deletedEntity.State = EntityState.Deleted;
        await Context.SaveChangesAsync();
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        return filter == null ? await Context.Set<TEntity>().ToListAsync() : await Context.Set<TEntity>().Where(filter).ToListAsync();
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await Context.Set<TEntity>().Where(filter).SingleOrDefaultAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var updatedEntity = Context.Entry(entity);
        updatedEntity.State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }
}
