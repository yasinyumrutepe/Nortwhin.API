
using MassTransit;
using Northwind.Core.Models.Request.ProductService;
using Northwind.Product.Consumer.Abstract;

namespace Northwind.Product.Consumer
{
    public class DeleteProductConsumer : IConsumer<DeleteProductConsumerModel>
    {
        private readonly IProductConsumerService _productConsumerService;

        public DeleteProductConsumer(IProductConsumerService productConsumerService) => _productConsumerService = productConsumerService;
        public async Task Consume(ConsumeContext<DeleteProductConsumerModel> context)
        {

          var  productID = context.Message.ProductID;

          var isDeleted =  await _productConsumerService.DeleteProductAsync(productID);
               

            await context.RespondAsync(isDeleted);

        }
    }
}
