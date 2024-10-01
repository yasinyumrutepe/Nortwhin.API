

using Microsoft.AspNetCore.Http;
using Northwind.Core.Models.Request.ProductImage;
using Northwind.Entities.Concrete;

namespace Northwind.Business.Abstract
{
    public interface IProductImageService
    {
        public Task<int> DeleteProductImageAsync(int productImageID);
        public Task<List<ProductImage>> InsertProductImageAsync(ProductImageRequestModel productImageRequest);
    }
}
