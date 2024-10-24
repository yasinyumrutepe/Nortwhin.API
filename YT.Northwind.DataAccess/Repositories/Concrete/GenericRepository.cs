using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Response;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Query;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace Northwind.DataAccess.Repositories.Concrete
{
    public class GenericRepository<TEntity, TContext>(TContext context) : IGenericRepository<TEntity>
        where TEntity : class, new()
        where TContext : DbContext
    {
        private readonly TContext _context = context;
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();


        public async Task<PaginatedResponse<TEntity>> GetAllAsync(
        PaginatedRequest paginatedRequest,
        Expression<Func<TEntity, bool>> filter = null,
        params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
           
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var count = await query.CountAsync();

            if ( paginatedRequest == null)
            {
                var data = await query.ToListAsync();
                return new PaginatedResponse<TEntity>(data, 1, count, 1, count);
            }
            else
            {
                var totalPages = (int)Math.Ceiling((double)count / paginatedRequest.Limit);
                var data = await query
                    .Skip((paginatedRequest.Page - 1) * paginatedRequest.Limit)
                    .Take(paginatedRequest.Limit)
                    .ToListAsync();

                return new PaginatedResponse<TEntity>(data, paginatedRequest.Page, paginatedRequest.Limit, totalPages, count);
            }
        }


        public async Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {   

            IQueryable<TEntity> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
           return await query.ToListAsync();
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = include(query);
            }
            try
            {

                var data = await query.FirstOrDefaultAsync();
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return null;
            }
        }
      
        public async Task<TEntity> AddAsync(TEntity entity)
        {   
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.Message;
                Console.WriteLine(innerException);
                return null;
            }
           
          
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
            }catch (DbUpdateException ex) {
                var innerException = ex.InnerException?.Message;
                Console.WriteLine(innerException);
                return null;
            }

        }
        public async Task<int> DeleteAsync(TEntity entity)
        {
            try
            {
                 _context.Set<TEntity>().Remove(entity);
                return await _context.SaveChangesAsync();
            
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                Console.WriteLine(innerException);
                return 0;
            }

        }
        public async Task<PaginatedResponse<TEntity>> GetAllAsync2(
            PaginatedRequest paginatedRequest = null,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> filterFunc = null)
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;

                if (disableTracking)
                {
                    query = query.AsNoTracking();
                }

                if (include != null)
                {
                    query = include(query);
                }

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                if (filterFunc != null)
                {
                    query = filterFunc(query);
                }

                var count = await query.CountAsync();

                if (paginatedRequest == null || paginatedRequest.Page == 0)
                {
                    var data = await query.ToListAsync();
                    return new PaginatedResponse<TEntity>(data, 1, count, 1, count);
                }
                else
                {
                    var totalPages = (int)Math.Ceiling((double)count / paginatedRequest.Limit);

                    if (orderBy != null)
                    {
                        var data = await orderBy(query)
                            .Skip((paginatedRequest.Page - 1) * paginatedRequest.Limit)
                            .Take(paginatedRequest.Limit)
                            .ToListAsync();

                        return new PaginatedResponse<TEntity>(data, paginatedRequest.Page, paginatedRequest.Limit, totalPages, count);
                    }
                    else
                    {
                        var data = await query
                            .Skip((paginatedRequest.Page - 1) * paginatedRequest.Limit)
                            .Take(paginatedRequest.Limit)
                            .ToListAsync();

                        return new PaginatedResponse<TEntity>(data, paginatedRequest.Page, paginatedRequest.Limit, totalPages, count);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return new PaginatedResponse<TEntity>(new List<TEntity>(), 1, 0, 1, 0);
            }
        }




    }
}
