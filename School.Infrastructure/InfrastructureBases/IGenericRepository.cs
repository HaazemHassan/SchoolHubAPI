using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace School.Infrastructure.InfrastructureBases
{
    public interface IGenericRepository<T> where T : class
    {
        Task DeleteRangeAsync(ICollection<T> entities);
        Task<T?> GetByIdAsync(int id);
        Task SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        void Commit();
        void RollBack();
        IQueryable<T> GetTableNoTracking(Expression<Func<T, bool>>? predicate = null);
        IQueryable<T> GetTableAsTracking(Expression<Func<T, bool>>? predicate = null);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task DeleteAsync(T entity);

        Task<T?> Get(Expression<Func<T, bool>> predicate);

    }
}
