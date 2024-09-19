
using AutoMapper;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Product;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Product;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;


namespace Northwind.Business.Concrete
{
    public class ProductService(IProductRepository productRepository,IMapper mapper) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<PaginatedResponse<ProductResponseModel>> GetAllProductAsync(PaginatedRequest paginated)
        {
            var productsPaginated =  await _productRepository.GetAllAsync(paginated,null,p=>p.Category);
            var responseDto = _mapper.Map<PaginatedResponse<ProductResponseModel>>(productsPaginated);
            return responseDto;


        }

        public async Task<ProductResponseModel> GetProductAsync(int id)
        {
            var productEntity = await _productRepository.GetAsync(p => p.ProductID == id);
            return _mapper.Map<ProductResponseModel>(productEntity); 
        }
        public async Task<ProductResponseModel> AddProductAsync(ProductRequestModel product)
        {
            var productEntity = _mapper.Map<Product>(product);
            var addedProduct = await _productRepository.AddAsync(productEntity);
            var responseDto = _mapper.Map<ProductResponseModel>(addedProduct);
            return responseDto;
        }
        public async Task<ProductResponseModel> UpdateProductAsync(ProductUpdateRequestModel product)
        {

            var updatedProduct = _mapper.Map<Product>(product);
            return  _mapper.Map<ProductResponseModel>(await _productRepository.UpdateAsync(updatedProduct));
        }

        public async Task<int> DeleteProductAsync(int id)
        {
          return  await _productRepository.DeleteAsync(id);
        }
    }
}
