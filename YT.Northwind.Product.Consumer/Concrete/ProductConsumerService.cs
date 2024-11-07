using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Product.Consumer.Models.Request;
using Northwind.Product.Consumer.Abstract;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.ProductService;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

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
                UnitPrice = product.UnitPrice,
                Description = product.Description,
                ProductImages = productImages,

            };

            foreach (var size in product.Size)
            {
             productEntities.ProductVariants.Add(new Entities.Concrete.ProductVariant
             {
                 VariantID = size
             });
            }

            productEntities.ProductVariants.Add(new Entities.Concrete.ProductVariant
            {
                VariantID = product.Color
            });


            return  await _productRepository.AddAsync(productEntities);

           

        }

       
       

      

        public async Task<Entities.Concrete.Product> UpdateProductAsync(UpdateProductConsumerRequest product)
        {
         var updatedProduct= await _productRepository.UpdateAsync(new Entities.Concrete.Product
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                UnitPrice = product.UnitPrice,
                Description = product.Description,
                UnitsInStock = product.UnitsInStock
         });

            return updatedProduct;

        }

        public async Task<DeleteProductConsumerResponseModel> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetAsync(p => p.ProductID == id);
            var isDeleted = await _productRepository.DeleteAsync(product);
            return new DeleteProductConsumerResponseModel
            {
                IsDeleted = isDeleted
            };
        }

       
    }
}
