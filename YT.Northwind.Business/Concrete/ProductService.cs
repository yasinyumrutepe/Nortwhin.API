
using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
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
    public class ProductService(IMapper mapper, IBus bus,ICloudinaryService cloudinaryService,ITokenService tokenService,IProductRepository productRepository,ICategoryRepository categoryRepository) : IProductService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IBus _bus = bus;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICloudinaryService _cloudinaryService = cloudinaryService;
        private readonly ITokenService _tokenService = tokenService;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        #region GetAllProducts
        public async Task<PaginatedResponse<ProductResponseModel>> GetAllProductAsync(AllProductRequestModel productFilter,string token="")
        {

            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null;
            Func<IQueryable<Product>, IIncludableQueryable<Product, object>> includeExpression;

            IQueryable<Product> filter(IQueryable<Product> query)
            {
                if (productFilter.ProductFilterKeys == null) return query;

                if (!string.IsNullOrEmpty(productFilter.ProductFilterKeys.Categories))
                {
                    var categoryIds = productFilter.ProductFilterKeys.Categories
                        .Split(',')
                        .Select(id => int.Parse(id.Trim()))
                        .ToList();

                    query = query.Where(p => p.ProductCategories.Any(pc => categoryIds.Contains(pc.CategoryID)));
                  
                }

                if (productFilter.ProductFilterKeys.MinPrice > 0)
                {
                    query = query.Where(p => p.UnitPrice >= productFilter.ProductFilterKeys.MinPrice);
                }

                if (productFilter.ProductFilterKeys.MaxPrice > 0)
                {
                    query = query.Where(p => p.UnitPrice <= productFilter.ProductFilterKeys.MaxPrice);
                }

                if(!string.IsNullOrEmpty(productFilter.ProductFilterKeys.Colors))
                {
                    var colors = productFilter.ProductFilterKeys.Colors
                       .Split(',')
                       .Select(id => int.Parse(id.Trim()))
                       .ToList();
                    query = query.Where(p => p.ProductVariants.Any(pc => colors.Contains(pc.VariantID)));
                }

                if(!string.IsNullOrEmpty(productFilter.ProductFilterKeys.Sizes))
                {
                    var sizes = productFilter.ProductFilterKeys.Sizes
                       .Split(',')
                       .Select(id => int.Parse(id.Trim()))
                       .ToList();
                    query = query.Where(p => p.ProductVariants.Any(pc => sizes.Contains(pc.VariantID)));
                }

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
                "bestSelling" => q => q.OrderBy(p => p.UnitPrice),
                _ => q => q.OrderBy(p => p.ProductID),
            };
            Console.WriteLine(token);
            if (string.IsNullOrEmpty(token))
            {
                includeExpression = p => p
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductCategories).ThenInclude(p=>p.Category)
                    .Include(p => p.ProductReviews);
            }
            else
            {
                includeExpression = p => p
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductCategories).ThenInclude(p => p.Category)
                    .Include(p => p.ProductReviews)
                    .Include(p => p.ProductFavorites.Where(c => c.CustomerID == _tokenService.GetCustomerIDClaim(token)));
            }


            var products = await _productRepository.GetAllAsync2(
                productFilter.PaginatedRequest,
                include: includeExpression,
                filterFunc: filter,
                orderBy: orderBy
            );

            return _mapper.Map<PaginatedResponse<ProductResponseModel>>(products);



        }
        #endregion

        #region GetProducts
        public async Task<ProductResponseModel> GetProductAsync(int id,string token)
        {
            Func<IQueryable<Product>, IIncludableQueryable<Product, object>> includeExpression;
            
            if (string.IsNullOrEmpty(token))
                {
                includeExpression = p => p
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductReviews)
                    .Include(p => p.ProductVariants)
                    .Include(p => p.ProductCategories).ThenInclude(p => p.Category);
            }
            else
            {
                includeExpression = p => p
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductReviews)
                    .Include(p => p.ProductCategories).ThenInclude(p=>p.Category)
                    .Include(p => p.ProductVariants).ThenInclude(p=>p.Variant)
                    .Include(p => p.ProductFavorites.Where(c => c.CustomerID == _tokenService.GetCustomerIDClaim(token)));
            }

            var product = await _productRepository.GetAsync(p => p.ProductID == id, include: includeExpression);
            return _mapper.Map<ProductResponseModel>(product);
        }
        #endregion

        #region GetProductsByCategory
        public async Task<PaginatedResponse<ProductResponseModel>> GetProductsByCategory(AllProductRequestModel productFilter, string token)
        {
            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null;
            Func<IQueryable<Product>, IIncludableQueryable<Product, object>> includeExpression;

            int categoryID=0;

            if (!string.IsNullOrEmpty(productFilter.ProductFilterKeys.Slug))
            {

                var category = await _categoryRepository.GetAsync(c => c.Slug == productFilter.ProductFilterKeys.Slug);

                if (category != null)
                {
                    categoryID = category.CategoryID;
                }
            }


            IQueryable<Product> filter(IQueryable<Product> query)
            {
                if (productFilter.ProductFilterKeys == null) return query;

                if (!string.IsNullOrEmpty(productFilter.ProductFilterKeys.Categories))
                {
                    var categoryIds = productFilter.ProductFilterKeys.Categories
                        .Split(',')
                        .Select(id => int.Parse(id.Trim()))
                        .ToList();
                    categoryIds.Add(categoryID);
               
                    query = query.Where(p => p.ProductCategories.Any(pc => categoryIds.Contains(pc.CategoryID))  );

                }else
                {
                    query = query.Where(p => p.ProductCategories.Any(pc=>pc.CategoryID == categoryID));
                }

                if (productFilter.ProductFilterKeys.MinPrice > 0)
                {
                    query = query.Where(p => p.UnitPrice >= productFilter.ProductFilterKeys.MinPrice);
                }

                if (productFilter.ProductFilterKeys.MaxPrice > 0)
                {
                    query = query.Where(p => p.UnitPrice <= productFilter.ProductFilterKeys.MaxPrice);
                }

                if (!string.IsNullOrEmpty(productFilter.ProductFilterKeys.Colors))
                {
                    var colors = productFilter.ProductFilterKeys.Colors
                       .Split(',')
                       .Select(id => int.Parse(id.Trim()))
                       .ToList();
                    query = query.Where(p => p.ProductVariants.Any(pc => colors.Contains(pc.VariantID)));
                }

                if (!string.IsNullOrEmpty(productFilter.ProductFilterKeys.Sizes))
                {
                    var sizes = productFilter.ProductFilterKeys.Sizes
                       .Split(',')
                       .Select(id => int.Parse(id.Trim()))
                       .ToList();
                    query = query.Where(p => p.ProductVariants.Any(pc => sizes.Contains(pc.VariantID)));
                }

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
            if (string.IsNullOrEmpty(token))
            {
                includeExpression = p => p
                    .Include(p => p.ProductImages)
                      .Include(p => p.ProductCategories).ThenInclude(p => p.Category)
                    .Include(p => p.ProductReviews);
            }
            else
            {
                includeExpression = p => p
                    .Include(p => p.ProductImages)
                      .Include(p => p.ProductCategories).ThenInclude(p => p.Category)
                    .Include(p => p.ProductReviews)
                    .Include(p => p.ProductFavorites.Where(c => c.CustomerID == _tokenService.GetCustomerIDClaim(token)));
            }


            var products = await _productRepository.GetAllAsync2(
                productFilter.PaginatedRequest,
                include: includeExpression,
                filterFunc: filter,
                orderBy: orderBy
            );

            return _mapper.Map<PaginatedResponse<ProductResponseModel>>(products);


        }
        #endregion

        #region AddProduct
        public async Task<string> AddProductAsync(ProductRequestModel product)
        {

          var imagesResult = await _cloudinaryService.UploadImageAsync(product.Images,"Northwind");

            List<int> categories = product.Categories.Split(',')
                              .Select(int.Parse)
                              .ToList();
            List<int> sizes = product.Size.Split(',')
                .Select(int.Parse)
                .ToList();

            var productConsumerModel = new CreateProductConsumerModel
            {
                ProductName = product.ProductName,
                Categories = categories,
                UnitPrice = product.UnitPrice,
                Description = product.Description,
                Images = imagesResult,
                Color = product.Color,
                Size = sizes,
            };
            await _bus.Send(productConsumerModel);


            return "Product Added Send Queue";
        }
        #endregion

        #region UpdateProductAsync
        public async Task<string> UpdateProductAsync(ProductUpdateRequestModel product)
        {
            var updatedProduct = new UpdateProductConsumerModel
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Categories = product.Categories,
                UnitPrice = product.UnitPrice,
                Description = product.Description,
                UnitsInStock = product.UnitsInStock,
                Sizes = product.Sizes,
                Color = product.Colors
            };

            await _bus.Send(updatedProduct);

            return "Product Updated Send Queue";
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
