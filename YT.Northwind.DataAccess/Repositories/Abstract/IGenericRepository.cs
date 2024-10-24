

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Response;


namespace Northwind.DataAccess.Repositories.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {   
        Task<PaginatedResponse<TEntity>> GetAllAsync(PaginatedRequest paginatedRequest= null, Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes);

        Task<PaginatedResponse<TEntity>> GetAllAsync2(PaginatedRequest paginatedRequest = null,
    Expression<Func<TEntity, bool>> predicate = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
    bool disableTracking = true,
     Func<IQueryable<TEntity>, IQueryable<TEntity>> filterFunc = null);


        Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
    }
}
