
using MassTransit;
using Northwind.Core.Models.Request.ProductService;
using Northwind.Entities.Concrete;
using Northwind.Product.Consumer.Abstract;

namespace Northwind.Product.Consumer
{
    public class GetProductConsumer : IConsumer<GetProductConsumerModel>
    {
        private readonly IProductConsumerService _productConsumerService;

        public GetProductConsumer(IProductConsumerService productConsumerService) => _productConsumerService = productConsumerService;
        public async Task Consume(ConsumeContext<GetProductConsumerModel> context)
        {
            var productID = context.Message.ProductID;
            var product =  await  _productConsumerService.GetProductAsync(productID);
            await context.RespondAsync(product);
        }
    }
}
