

using AutoMapper;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request.ProductReview;
using Northwind.Core.Models.Response.ProductReview;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;

namespace Northwind.Business.Concrete
{
    public class ProductReviewService(IProductReviewRepository productReviewRepository, IMapper mapper,ITokenService tokenService) : IProductReviewService
    {
        private readonly IProductReviewRepository _productReviewRepository = productReviewRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<ProductReviewResponseModel> AddProductRewiewAsync(ProductReviewRequestModel productRewiewRequestModel,string token)
        {   

            var customerID = _tokenService.GetCustomerIDClaim(token);
            var productRev = _mapper.Map<ProductReview>(productRewiewRequestModel);
            productRev.CustomerID = customerID;
            var addProductRev =  await _productReviewRepository.AddAsync(productRev);
            return _mapper.Map<ProductReviewResponseModel>(addProductRev);
        }
    }
}
