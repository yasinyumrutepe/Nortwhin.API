using  Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Product;
using Northwind.Core.Models.Request.ProductService;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Product;


namespace Northwind.Business.Abstract
{
    public interface IProductService
    {
        Task<PaginatedResponse<ProductResponseModel>> GetAllProductAsync(AllProductRequestModel productFilter,string customerID);

        Task<ProductResponseModel> GetProductAsync(int id, string customerID);

        Task<PaginatedResponse<ProductResponseModel>> GetProductsByCategory(AllProductRequestModel productFilter, string token);
        Task<string> AddProductAsync(ProductRequestModel product);
        Task<string> UpdateProductAsync(ProductUpdateRequestModel product);
        Task<int> DeleteProductAsync(int id);
    }
}
