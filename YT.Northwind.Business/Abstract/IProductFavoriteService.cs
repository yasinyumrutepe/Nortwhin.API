

using Northwind.Core.Models.Request.ProductFavorite;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Customer;
using Northwind.Core.Models.Response.Product;
using Northwind.Core.Models.Response.ProductFavorite;
using Northwind.Entities.Concrete;

namespace Northwind.Business.Abstract
{
    public interface IProductFavoriteService
    {
        Task<PaginatedResponse<ProductFavorite>> GetCustomerFavorites(string token);
        Task<ProductFavoriteResponseModel> AddProductFavoriteAsync(ProductFavoriteRequestModel productFavoriteRequestModel, string token);
        Task<int> DeleteProductFavoriteAsync(int productID, string token);
    }
}
