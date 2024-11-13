using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Product.Consumer.Models.Request;
using Northwind.Product.Consumer.Abstract;
using Northwind.Core.Models.Response.ProductService;
using Microsoft.EntityFrameworkCore;

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

            var productCategories = new List<Entities.Concrete.ProductCategory>();

            foreach (var category in product.Categories)
            {
                productCategories.Add(new Entities.Concrete.ProductCategory
                {
                    CategoryID = category
                });
            }

            var variants = new List<Entities.Concrete.ProductVariant>();
            foreach (var variant in product.Size)
            {

                variants.Add(new Entities.Concrete.ProductVariant
                {
                    VariantID = variant
                });

            }
            variants.Add(new Entities.Concrete.ProductVariant
            {
                VariantID = product.Color
            });

            var productEntities = new Entities.Concrete.Product
            {
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                Description = product.Description,
                ProductImages = productImages,
                ProductCategories = productCategories,
                ProductVariants = variants,
                CreatedAt = DateTime.Now,

            };

            return  await _productRepository.AddAsync(productEntities);
        }
        public async Task<Entities.Concrete.Product> UpdateProductAsync(UpdateProductConsumerRequest product)
        {
             var dbProduct = await _productRepository.GetAsync(p => p.ProductID == product.ProductID,include:p =>p.Include(p=>p.ProductCategories).Include(p=>p.ProductVariants));

            if (dbProduct == null)
            {
                return null;
            }


            dbProduct.ProductCategories.Clear();
            dbProduct.ProductVariants.Clear();


            var productCategories = new List<Entities.Concrete.ProductCategory>();
            foreach (var category in product.Categories) {
                productCategories.Add(new Entities.Concrete.ProductCategory
                {
                    CategoryID = category
                });
            }

            var productVariants = new List<Entities.Concrete.ProductVariant>();
            foreach (var variant in product.Sizes)
            {
                productVariants.Add(new Entities.Concrete.ProductVariant
                {
                    VariantID = variant
                });
            }
            if (product.Color != 0)
            {
                productVariants.Add(new Entities.Concrete.ProductVariant
                {
                    VariantID = product.Color
                });
            }
          

            dbProduct.ProductID = product.ProductID;
            dbProduct.ProductCategories = productCategories;
            dbProduct.ProductVariants = productVariants;
            dbProduct.ProductName = product.ProductName;
            dbProduct.UnitPrice = product.UnitPrice;
            dbProduct.Description = product.Description;
            dbProduct.UnitsInStock = product.UnitsInStock;
            dbProduct.CreatedAt = DateTime.Now;
            var updatedProduct = await _productRepository.UpdateAsync(dbProduct);
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
