
using AutoMapper;
using MassTransit;
using MassTransit.Clients;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Product;
using Northwind.Core.Models.Request.ProductService;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Product;
using Northwind.Core.Models.Response.ProductService;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;


namespace Northwind.Business.Concrete
{
    public class ProductService(IProductRepository productRepository,IMapper mapper, IBus bus,ICloudinaryService cloudinaryService) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IBus _bus = bus;
        private readonly ICloudinaryService _cloudinaryService = cloudinaryService;

        public async Task<PaginatedResponse<ProductResponseModel>> GetAllProductAsync(PaginatedRequest paginated)
        {
            var request =  _bus.CreateRequestClient<GetAllProductConsumerModel>();
            var response = await request.GetResponse<PaginatedResponse<Product>>(new GetAllProductConsumerModel
            {
                Page = paginated.Page,
                Limit = paginated.Limit
            });

              return  _mapper.Map<PaginatedResponse<ProductResponseModel>>(response.Message);
        }

        public async Task<ProductResponseModel> GetProductAsync(int id)
        {
            var request = _bus.CreateRequestClient<GetProductConsumerModel>();
            var response = await request.GetResponse<Product>(new GetProductConsumerModel
            {
               ProductID = id
            });
            return _mapper.Map<ProductResponseModel>(response.Message); 
        }
        public async Task<string> AddProductAsync(ProductRequestModel product)
        {

          var imagesResult = await _cloudinaryService.UploadImageAsync(product.Images,"Northwind");
                
            var productConsumerModel = new CreateProductConsumerModel
            {
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                Description = product.Description,
                Images = imagesResult,
            };
            await _bus.Send(productConsumerModel);


            return "Product Added Send Queue";
        }
        public async Task<ProductResponseModel> UpdateProductAsync(ProductUpdateRequestModel product)
        {
            var updateReq = _bus.CreateRequestClient<UpdateProductConsumerModel>();
            var updateRes = await updateReq.GetResponse<Product>(new UpdateProductConsumerModel
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                Description = product.Description,
            });

            return  _mapper.Map<ProductResponseModel>(updateRes.Message);
        }

        public async Task<int> DeleteProductAsync(int id)
        {

            var request = _bus.CreateRequestClient<DeleteProductConsumerModel>();
            var response = await request.GetResponse<DeleteProductConsumerResponseModel>(new DeleteProductConsumerModel
            {
                ProductID = id
            });
            return response.Message.IsDeleted;
        }
    }
}
