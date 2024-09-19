

using System.Linq.Expressions;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Response;


namespace Northwind.DataAccess.Repositories.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {   
        Task<PaginatedResponse<TEntity>> GetAllAsync(PaginatedRequest paginatedRequest, Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetAsync(Expression<Func<TEntity,bool>> predicate);

        Task<TEntity> GetAsync(int id);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(int id);
    }
}
