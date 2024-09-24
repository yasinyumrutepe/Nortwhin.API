using Northwind.DataAccess.Extensions;
using Northwind.Business.Extensions;
using MassTransit;
using Northwind.Product.Consumer;
using Northwind.Product.Consumer.Extensions;
using Northwind.Core.Models.Request.ProductService;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateProductConsumer>();
    x.AddConsumer<DeleteProductConsumer>();
    x.AddConsumer<UpdateProductConsumer>();
    x.AddConsumer<GetProductConsumer>();
    x.AddConsumer<GetAllProductConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("create-product-queue", e =>
        {
            e.ConfigureConsumer<CreateProductConsumer>(context);
        });

        cfg.ReceiveEndpoint("delete-product-queue", e =>
        {
            e.ConfigureConsumer<DeleteProductConsumer>(context);
        });

        cfg.ReceiveEndpoint("update-product-queue", e =>
        {
            e.ConfigureConsumer<UpdateProductConsumer>(context);
        });

        cfg.ReceiveEndpoint("get-product-queue", e =>
        {
            e.ConfigureConsumer<GetProductConsumer>(context);
        });

        cfg.ReceiveEndpoint("get-all-product-queue", e =>
        {
            e.ConfigureConsumer<GetAllProductConsumer>(context);
        });

        EndpointConvention.Map<CreateProductConsumerModel>(new Uri("queue:create-product-queue"));
        EndpointConvention.Map<DeleteProductConsumerModel>(new Uri("queue:delete-product-queue"));
        EndpointConvention.Map<UpdateProductConsumerModel>(new Uri("queue:update-product-queue"));
        EndpointConvention.Map<GetProductConsumerModel>(new Uri("queue:get-product-queue"));
        EndpointConvention.Map<GetAllProductConsumerModel>(new Uri("queue:get-all-product-queue"));



    });
});

builder.Services.AddMassTransitHostedService();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataAccessRegisration();
builder.Services.AddBusinessRegistration();
builder.Services.AddProductServiceRegisration();
builder.Services.AddAuthorizationBuilder();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
