
using MassTransit;
using Northwind.Core.Models.Request.ProductService;
using Northwind.Product.Consumer.Abstract;
using Northwind.Product.Consumer.Models.Request;

namespace Northwind.Product.Consumer
{
    public class CreateProductConsumer : IConsumer<CreateProductConsumerModel>
    {
        private readonly IProductConsumerService _productConsumerService;
        public CreateProductConsumer(IProductConsumerService productConsumerService) => _productConsumerService = productConsumerService;
        public async Task Consume(ConsumeContext<CreateProductConsumerModel> context)
        {
            var newProduct = context.Message;
            try
            {
               await _productConsumerService.AddProductAsync(new AddProductConsumerRequest
                {
                    ProductName = newProduct.ProductName,
                    CategoryID = newProduct.CategoryID,
                    UnitPrice = newProduct.UnitPrice,
                    Description = newProduct.Description,
                    ProductImages = newProduct.Images,
                   Size = newProduct.Size,
                   Color = newProduct.Color
               });
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Task.FromException(new Exception("Product could not be added"));
            }
        }
    }
}
