
using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    public class ProductService(IMapper mapper, IBus bus,ICloudinaryService cloudinaryService,ITokenService tokenService,IProductRepository productRepository) : IProductService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IBus _bus = bus;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICloudinaryService _cloudinaryService = cloudinaryService;
        private readonly ITokenService _tokenService = tokenService;

        #region GetAllProducts
        public async Task<PaginatedResponse<ProductResponseModel>> GetAllProductAsync(AllProductRequestModel productFilter,string token)
        {

            var customerID = _tokenService.GetCustomerIDClaim(token);

            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null;

            IQueryable<Product> filter(IQueryable<Product> query)
            {
                if (productFilter.ProductFilterKeys == null) return query;

                // Kategori filtreleme
                if (!string.IsNullOrEmpty(productFilter.ProductFilterKeys.Categories))
                {
                    var categoryIds = productFilter.ProductFilterKeys.Categories
                        .Split(',')
                        .Select(id => int.Parse(id.Trim()))
                        .ToList();

                    query = query.Where(p => categoryIds.Contains(p.CategoryID));
                }

                // Fiyat filtreleme
                if (productFilter.ProductFilterKeys.MinPrice > 0)
                {
                    query = query.Where(p => p.UnitPrice >= productFilter.ProductFilterKeys.MinPrice);
                }

                if (productFilter.ProductFilterKeys.MaxPrice > 0)
                {
                    query = query.Where(p => p.UnitPrice <= productFilter.ProductFilterKeys.MaxPrice);
                }

                // Rating filtreleme
                if (!string.IsNullOrEmpty(productFilter.ProductFilterKeys.Ratings))
                {
                    var ratingValues = productFilter.ProductFilterKeys.Ratings
                        .Split(',')
                        .Select(r => decimal.Parse(r.Trim()))
                        .ToList();

                    query = query.Where(p =>
                        p.ProductReviews.Any() && 
                        ratingValues.Contains(p.ProductReviews.Average(r => r.Star))
                    );
                }

                return query;
            }

            orderBy = productFilter.OrderByKey switch
            {
                "priceAsc" => q => q.OrderBy(p => p.UnitPrice),
                "priceDesc" => q => q.OrderByDescending(p => p.UnitPrice),
                "newest" => q => q.OrderByDescending(p => p.CreatedAt),
                _ => q => q.OrderBy(p => p.ProductID),
            };
            var products = await _productRepository.GetAllAsync2(
                    productFilter.PaginatedRequest,
                    include: p => p.Include(p => p.ProductImages)
                   .Include(p => p.ProductReviews)
                   .Include(p => p.ProductFavorites.Where(c => c.CustomerID == customerID)),
                    filterFunc: filter,
                    orderBy:orderBy);

            return _mapper.Map<PaginatedResponse<ProductResponseModel>>(products);
        }
        #endregion

        #region GetProducts
        public async Task<ProductResponseModel> GetProductAsync(int id,string token)
        {

            var customerID = _tokenService.GetCustomerIDClaim(token);
            var product = await _productRepository.GetAsync(p => p.ProductID == id, include: i => i.Include(p => p.ProductImages).Include(i => i.ProductReviews).Include(i => i.ProductFavorites.Where(c => c.CustomerID == customerID)));
            return _mapper.Map<ProductResponseModel>(product); 
        }
        #endregion

        #region GetProductsByCategory
        public async Task<PaginatedResponse<ProductResponseModel>> GetProductsByCategory(CategoryProductsRequest categoryProductsRequest, string token)
        {
            var customerID = _tokenService.GetCustomerIDClaim(token);
            var paginatedRequest = new PaginatedRequest
            {
                Page = categoryProductsRequest.Page,
                Limit = categoryProductsRequest.Limit
            };
           var products = await _productRepository.GetAllAsync(paginatedRequest, p => p.CategoryID == categoryProductsRequest.CategoryID, i => i.ProductImages, i => i.ProductFavorites.Where(c => c.CustomerID == customerID));
            return _mapper.Map<PaginatedResponse<ProductResponseModel>>(products);
        }
        #endregion

        #region AddProduct
        public async Task<string> AddProductAsync(ProductRequestModel product)
        {

          var imagesResult = await _cloudinaryService.UploadImageAsync(product.Images,"Northwind");
                
            var productConsumerModel = new CreateProductConsumerModel
            {
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                UnitPrice = product.UnitPrice,
                Description = product.Description,
                Images = imagesResult,
            };
            await _bus.Send(productConsumerModel);


            return "Product Added Send Queue";
        }
        #endregion

        #region UpdateProductAsync
        public async Task<ProductResponseModel> UpdateProductAsync(ProductUpdateRequestModel product)
        {
            var updateReq = _bus.CreateRequestClient<UpdateProductConsumerModel>();
            var updateRes = await updateReq.GetResponse<Product>(new UpdateProductConsumerModel
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                UnitPrice = product.UnitPrice,
                Description = product.Description,
                UnitsInStock = product.UnitsInStock

            });

            return  _mapper.Map<ProductResponseModel>(updateRes.Message);
        }

        #endregion

        #region DeleteProduct
        public async Task<int> DeleteProductAsync(int id)
        {

            var request = _bus.CreateRequestClient<DeleteProductConsumerModel>();
            var response = await request.GetResponse<DeleteProductConsumerResponseModel>(new DeleteProductConsumerModel
            {
                ProductID = id
            });
            return response.Message.IsDeleted;
        }
        #endregion



    }
}
