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
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", h =>
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

        EndpointConvention.Map<CreateProductConsumerModel>(new Uri("queue:create-product-queue"));
        EndpointConvention.Map<DeleteProductConsumerModel>(new Uri("queue:delete-product-queue"));
        EndpointConvention.Map<UpdateProductConsumerModel>(new Uri("queue:update-product-queue"));
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
    options.AddPolicy(name: "AllowAllOrigins",
         configurePolicy: policy =>
         {
             policy.AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod();
         });
    options.AddPolicy(name: "AllowOnlySomeOrigins",
        configurePolicy: policy =>
        {   
            policy.WithOrigins("http://localhost:3000/",
                "https://localhost:3000/",
                "http://front.localhost:3000/",
                "https://front.locahost:3000"
                );
        });
});

Console.WriteLine("Hello World!");


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}
app.UseCors("AllowAllOrigins");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
