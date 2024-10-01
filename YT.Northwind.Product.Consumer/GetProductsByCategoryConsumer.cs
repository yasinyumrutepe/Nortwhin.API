

using MassTransit;
using Northwind.Core.Models.Request.ProductService;
using Northwind.Product.Consumer.Abstract;
using Northwind.Product.Consumer.Models.Request;

namespace Northwind.Product.Consumer
{
    public class GetProductsByCategoryConsumer : IConsumer<GetProductsByCategoryConsumerModel>
    {
        private readonly IProductConsumerService _productConsumerService;

        public GetProductsByCategoryConsumer(IProductConsumerService productConsumerService) => _productConsumerService = productConsumerService;

        public async Task Consume(ConsumeContext<GetProductsByCategoryConsumerModel> context)
        {
            var categoryProductsRequest = context.Message;
            var products = await _productConsumerService.GetProductsByCategoryAsync(new CategoryProductsConsumerRequest
            {
                CategoryID = categoryProductsRequest.CategoryID,
                Page = categoryProductsRequest.Page,
                Limit = categoryProductsRequest.Limit
            });

            await context.RespondAsync(products);
        }

    }
}
