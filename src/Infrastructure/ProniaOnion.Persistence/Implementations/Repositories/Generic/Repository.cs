using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstraction.Repositories.Generic;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Persistence.Implementations.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly DbSet<T> _table;
        private readonly AppDbContext _context;


        public Repository(AppDbContext context)
        {
            _table = context.Set<T>();
            _context = context;
        }

        public IQueryable<T> GetAll(bool IsTracking = true, bool IsDeleted = false, params string[] includes)
        {
            var query = _table.AsQueryable();

            query = _addIncludes(query, includes);

            if (IsDeleted) query = query.IgnoreQueryFilters();

            return IsTracking ? query : query.AsNoTracking();
        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderExpression = null, bool isDescenting = false, int skip = 0, int take = 0, bool isTracking = true, bool IsDeleted = false, params string[] include)
        {
            var query = _table.AsQueryable();
            if (expression != null) query = query.Where(expression);

            if (skip != 0) query = query.Skip(skip);

            if (take != 0) query = query.Take(take);

            query = _addIncludes(query, include);


            if (IsDeleted) query = query.IgnoreQueryFilters();

            return isTracking ? query : query.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id, bool IsTracking = true, bool IsDeleted = false, params string[] includes)
        {
            var query = _table.Where(x => x.Id == id).AsQueryable();

            query = _addIncludes(query, includes);

            if (!IsTracking) query = query.AsNoTracking();

            if (IsDeleted) query = query.IgnoreQueryFilters();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool IsTracking = true, bool IsDeleted = false, params string[] includes)
        {
            var query = _table.Where(expression).AsQueryable();

            query = _addIncludes(query, includes);

            if (!IsTracking) query = query.AsNoTracking();

            if (IsDeleted) query = query.IgnoreQueryFilters();

            return await query.FirstOrDefaultAsync();
        }




        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }


        public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression, bool IsDeleted = false)
        {
            return IsDeleted ? await _table.AnyAsync(expression) : await _table.IgnoreQueryFilters().AnyAsync(expression);


        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }


        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            
        }
        public void ReverseSoftDelete(T entity)
        {
            entity.IsDeleted = false;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        private IQueryable<T> _addIncludes(IQueryable<T> query, params string[] include)
        {
            if (include != null)
            {
                for (int i = 0; i < include.Length; i++)
                {
                    query = query.Include(include[i]);
                }
            }
            return query;
        }


    }
}
