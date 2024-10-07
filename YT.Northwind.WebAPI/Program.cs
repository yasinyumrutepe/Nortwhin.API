using Northwind.DataAccess.Extensions;
using Northwind.Business.Extensions;
using MassTransit;
using Northwind.Product.Consumer;
using Northwind.Product.Consumer.Extensions;
using Northwind.Core.Models.Request.ProductService;
using Stripe;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(
  

    ).AddJsonOptions(options =>
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
    x.AddConsumer<GetProductsByCategoryConsumer>();
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

        cfg.ReceiveEndpoint("get-products-by-category-queue", e =>
        {
            e.ConfigureConsumer<GetProductsByCategoryConsumer>(context);
        });

        EndpointConvention.Map<CreateProductConsumerModel>(new Uri("queue:create-product-queue"));
        EndpointConvention.Map<DeleteProductConsumerModel>(new Uri("queue:delete-product-queue"));
        EndpointConvention.Map<UpdateProductConsumerModel>(new Uri("queue:update-product-queue"));
        EndpointConvention.Map<GetProductConsumerModel>(new Uri("queue:get-product-queue"));
        EndpointConvention.Map<GetAllProductConsumerModel>(new Uri("queue:get-all-product-queue"));
        EndpointConvention.Map<GetProductsByCategoryConsumerModel>(new Uri("queue:get-products-by-category-queue"));



    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Swagger'a JWT token ekleme
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddDataAccessRegisration();
builder.Services.AddBusinessRegistration();
builder.Services.AddProductServiceRegisration();
builder.Services.AddAuthorizationBuilder();

StripeConfiguration.ApiKey = "sk_test_51OtaEARoV1dehEh0g9XquCA7DPSBEy10BEE70kmoNLY1DjbQ4kLxzYRGx8GAFdeX6fqssbark3JA0Mxw9MsGiNjw00cwWbmxsn";



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
