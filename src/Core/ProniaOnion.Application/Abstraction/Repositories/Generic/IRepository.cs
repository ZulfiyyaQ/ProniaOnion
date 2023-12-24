using ProniaOnion.Domain.Entities;
using System.Linq.Expressions;

namespace ProniaOnion.Application.Abstraction.Repositories.Generic
{
    public interface IRepository<T> where T:BaseEntity,new()
    {
        IQueryable<T> GetAll(bool IsTracking = true,
            bool IsDeleted = false,
            params string[] includes);
        IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null,
           Expression<Func<T, object>>? orderExpression = null,
            bool isDescenting = false,
            int skip = 0,
            int take = 0,
            bool isTracking = true,
             bool IsDeleted = false,
           params string[] include);
        Task<T> GetByIdAsync(int id,
            bool IsTracking = true,
            bool IsDeleted = false,
            params string[] includes);
        Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression,
            bool IsTracking = true,
            bool IsDeleted = false,
            params string[] includes);

        Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression, 
            bool IsDeleted = false );

        Task AddAsync(T entity);
        void Update(T entity);

        void Delete(T entity);
        void SoftDelete(T entity);
        void ReverseSoftDelete(T entity);
        Task SaveChangesAsync();
    }
}
