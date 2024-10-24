

using Northwind.Business.Abstract;
using Northwind.Core.Models.Request.ProductImage;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.DataAccess.Repositories.Concrete;
using Northwind.Entities.Concrete;

namespace Northwind.Business.Concrete
{
    public class ProductImageService: IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly ICloudinaryService _cloudinaryService;
        public ProductImageService(IProductImageRepository productImageRepository, ICloudinaryService cloudinaryService)
        {
            _productImageRepository = productImageRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<int> DeleteProductImageAsync(int productImageID)
        {   
            var productImage = await _productImageRepository.GetAsync(filter:p=>p.ProductImageID==productImageID);
            if (productImage == null) return 0;
            var  isDelete = await  _productImageRepository.DeleteAsync(productImage);
            if (isDelete == 0) return 0;

            var isImageDelete = await _cloudinaryService.DeleteImageAsync(productImage.ImagePublicID);

            if (isImageDelete.StatusCode != System.Net.HttpStatusCode.OK) return 0;

            return 1;
        }

        public async Task<List<ProductImage>> InsertProductImageAsync(ProductImageRequestModel productImageRequest)
        {
            var imagesResult = await _cloudinaryService.UploadImageAsync(productImageRequest.Images, "Northwind");
            var productImages = new List<ProductImage>();
            if (imagesResult == null) return null;

            foreach (var image in imagesResult)
            {
                var productImage = new ProductImage
                {
                    ProductID = productImageRequest.ProductID,
                    ImagePath = image.ImagePath,
                    ImagePublicID = image.PublicID,

                };

              var addedProductImage =  await _productImageRepository.AddAsync(productImage);
                productImages.Add(addedProductImage);


            }

            return productImages;
          

        }
    }
}
