using Microsoft.EntityFrameworkCore;
using ScrapDealer.Domain.Repositories.Base;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Shared.Abstractions.Domain;
using System.Linq.Expressions;

namespace ScrapDealer.Infrastructure.EF.Repositories.Base
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : Entity<Guid>
    {
        protected readonly WriteDbContext _context;

        public GenericRepository(WriteDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task CommitAsync()
            => _context.SaveChangesAsync();

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with ID {id} not found.");

            // If the entity implements ISoftDeletable, we perform a soft delete
            if (entity is ISoftDeletable softDeletableEntity)
            {
                softDeletableEntity.SoftDelete();
                _context.Set<T>().Update(entity); // Mark as updated
            }
            else
            {
                _context.Set<T>().Remove(entity); // Hard delete if no soft delete
            }

            await _context.SaveChangesAsync();
        }

        public async Task<T?> GetAsync(
                Expression<Func<T, bool>> predicate,
                Expression<Func<T, object>>? include = null)

        {
            IQueryable<T> query = _context.Set<T>();

            if (include != null)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }

}
