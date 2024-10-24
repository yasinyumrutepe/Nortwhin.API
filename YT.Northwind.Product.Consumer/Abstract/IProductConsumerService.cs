using Northwind.Core.Models.Request;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.ProductService;
using Northwind.Product.Consumer.Models.Request;

namespace Northwind.Product.Consumer.Abstract
{
    public interface IProductConsumerService
    {
        Task<Entities.Concrete.Product> AddProductAsync(AddProductConsumerRequest product);
        Task<Entities.Concrete.Product> UpdateProductAsync(UpdateProductConsumerRequest product);
        Task<DeleteProductConsumerResponseModel> DeleteProductAsync(int id);
    }
}
