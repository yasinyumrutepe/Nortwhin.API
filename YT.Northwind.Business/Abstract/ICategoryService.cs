

using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Category;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Category;

namespace Northwind.Business.Abstract
{
    public interface ICategoryService
    {
        Task<PaginatedResponse<CategoryResponseModel>> GetAllCategoriesAsync(PaginatedRequest paginated);
        Task<CategoryResponseModel> GetCategoryAsync(int id);
        Task<List<CategoryResponseModel>> AddCategoryAsync(List<CategoryRequestModel> category);
        Task<CategoryResponseModel> UpdateCategoryAsync(CategoryUpdateRequestModel category);
        Task<int> DeleteCategoryAsync(int id);

    }
}
