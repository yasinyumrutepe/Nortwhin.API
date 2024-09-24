
using MassTransit;
using Northwind.Core.Models.Request.ProductService;
using Northwind.Product.Consumer.Abstract;

namespace Northwind.Product.Consumer
{
    public class UpdateProductConsumer : IConsumer<UpdateProductConsumerModel>
    {
        private readonly IProductConsumerService _productConsumerService;

        public UpdateProductConsumer(IProductConsumerService productConsumerService) => _productConsumerService = productConsumerService;
        public async Task Consume(ConsumeContext<UpdateProductConsumerModel> context)
        {
            var product = context.Message;

          var updatedProduct = await _productConsumerService.UpdateProductAsync(new Models.Request.UpdateProductConsumerRequest
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                Description = product.Description,
            });

          await context.RespondAsync(updatedProduct);
        }
          
    }
}
