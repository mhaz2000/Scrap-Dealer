using ScrapDealer.Shared.Abstractions.Domain;
using System.Linq.Expressions;

namespace ScrapDealer.Domain.Repositories.Base
{
    public interface IGenericRepository<T> where T : Entity<Guid>
    {
        Task AddAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? include = null);
        Task UpdateAsync(T entity);
        Task CommitAsync();
    }

}
