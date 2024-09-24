
using MassTransit;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.ProductService;
using Northwind.Product.Consumer.Abstract;

namespace Northwind.Product.Consumer
{
    public class GetAllProductConsumer : IConsumer<GetAllProductConsumerModel> 
    {
        private readonly IProductConsumerService _productConsumerService;

        public GetAllProductConsumer(IProductConsumerService productConsumerService) => _productConsumerService = productConsumerService;
        public async Task Consume(ConsumeContext<GetAllProductConsumerModel> context)
        {   
            var data = context.Message;
            var paginateReq = new PaginatedRequest
            {
                Page = data.Page,
                Limit = data.Limit
            };
         var  products = await  _productConsumerService.GetAllProductAsync(paginateReq);
         await context.RespondAsync(products);
        }
    }
}
