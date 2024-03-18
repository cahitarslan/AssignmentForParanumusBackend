﻿using Entities.Abstract;
using System.Linq.Expressions;

namespace DataAccess.Abstract;

public interface IEntityRepositoryAsync<T> where T: class, IEntity, new()
{
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
    Task<T> GetAsync(Expression<Func<T, bool>> filter);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
