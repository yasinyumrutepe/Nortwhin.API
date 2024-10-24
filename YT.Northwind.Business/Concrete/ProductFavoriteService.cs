

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request.ProductFavorite;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Customer;
using Northwind.Core.Models.Response.Product;
using Northwind.Core.Models.Response.ProductFavorite;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;

namespace Northwind.Business.Concrete
{
    public class ProductFavoriteService : IProductFavoriteService
    {
        private readonly IProductFavoriteRepository _productFavoriteRepository;
        private readonly IProductRepository _productRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _map;

        public ProductFavoriteService(IProductFavoriteRepository productFavoriteRepository, ITokenService tokenService,IMapper mapper, IProductRepository productRepository)
        {
            _productFavoriteRepository = productFavoriteRepository;
            _tokenService = tokenService;
            _map = mapper;
            _productRepository = productRepository;
        }

        public async Task<ProductFavoriteResponseModel> AddProductFavoriteAsync(ProductFavoriteRequestModel productFavoriteRequestModel, string token)
        {
            var customerID = _tokenService.GetCustomerIDClaim(token);
            var productFavorite = _map.Map<ProductFavorite>(productFavoriteRequestModel);
            productFavorite.CustomerID = customerID;
            var newProductFavorite = await _productFavoriteRepository.AddAsync(productFavorite);
            return _map.Map<ProductFavoriteResponseModel>(newProductFavorite);
        }

        public async Task<int> DeleteProductFavoriteAsync(int productID, string token)
        {
           var customerID = _tokenService.GetCustomerIDClaim(token);
           var productFavorite = await _productFavoriteRepository.GetAsync(filter: x => x.ProductID == productID && x.CustomerID == customerID);
            var isDeleted= await _productFavoriteRepository.DeleteAsync(productFavorite);
         
           return isDeleted;
        }

        public async Task<PaginatedResponse<ProductFavorite>> GetCustomerFavorites(string token)
        {
           var customerID = _tokenService.GetCustomerIDClaim(token);
            var favorites = await _productFavoriteRepository.GetAllAsync2(predicate:c=>c.CustomerID == customerID,include:p=>p.Include(p=>p.Product).ThenInclude(p=>p.ProductImages));
            return favorites;
        }
    }
}
