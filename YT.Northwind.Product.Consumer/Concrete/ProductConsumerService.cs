using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Product.Consumer.Models.Request;
using Northwind.Product.Consumer.Abstract;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.ProductService;

namespace Northwind.Product.Consumer.Concrete
{
    public class ProductConsumerService(IProductRepository productRepository) : IProductConsumerService
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<Entities.Concrete.Product> AddProductAsync(AddProductConsumerRequest product)
        {   

            var productImages = new List<Entities.Concrete.ProductImage>();

            foreach (var image in product.ProductImages)
            {
                productImages.Add(new Entities.Concrete.ProductImage
                {
                    ImagePath = image.ImagePath,
                    ImagePublicID = image.PublicID,

                });
            }
            var productEntities = new Entities.Concrete.Product
            {
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                Description = product.Description,
                ProductImages = productImages
               
            };

          return  await _productRepository.AddAsync(productEntities);

           

        }

       
        public async Task<PaginatedResponse<Entities.Concrete.Product>> GetAllProductAsync(PaginatedRequest paginatedRequest)
        {   

         
         return  await _productRepository.GetAllAsync(paginatedRequest, null,p=>p.ProductImages);
        }

        public Task<Entities.Concrete.Product> GetProductAsync(int id)
        {
           return _productRepository.GetAsync(p => p.ProductID == id,i=>i.ProductImages);
        }

        public async Task<Entities.Concrete.Product> UpdateProductAsync(UpdateProductConsumerRequest product)
        {
         var updatedProduct= await _productRepository.UpdateAsync(new Entities.Concrete.Product
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                Description = product.Description,
            });

            return updatedProduct;

        }

        public async Task<DeleteProductConsumerResponseModel> DeleteProductAsync(int id)
        {
         var isDeleted = await _productRepository.DeleteAsync(id);
            return new DeleteProductConsumerResponseModel
            {
                IsDeleted = isDeleted
            };
        }

        public async Task<PaginatedResponse<Entities.Concrete.Product>> GetProductsByCategoryAsync(CategoryProductsConsumerRequest categoryProductsRequest)
        {
            var paginatedRequest = new PaginatedRequest
            {
                Limit = categoryProductsRequest.Limit,
                Page = categoryProductsRequest.Page
            };

           return  await _productRepository.GetAllAsync(paginatedRequest, p=>p.CategoryID == categoryProductsRequest.CategoryID, i => i.ProductImages);
        }
    }
}
