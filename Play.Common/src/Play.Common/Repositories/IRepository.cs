using System.Linq.Expressions;
using Play.Common.Entities;
namespace Play.Common.Repositories;

public interface IRepository<T> where T : IEntity
{
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task<T> GetAsync(Guid id);
    
    Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
    Task<T> GetAsync(Expression<Func<T, bool>> filter);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(Guid id);
}