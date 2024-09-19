using  Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Product;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Product;


namespace Northwind.Business.Abstract
{
    public interface IProductService
    {
        Task<PaginatedResponse<ProductResponseModel>> GetAllProductAsync(PaginatedRequest paginatedRequest);

        Task<ProductResponseModel> GetProductAsync(int id);
        Task<ProductResponseModel> AddProductAsync(ProductRequestModel product);
        Task<ProductResponseModel> UpdateProductAsync(ProductUpdateRequestModel product);
        Task<int> DeleteProductAsync(int id);
    }
}
