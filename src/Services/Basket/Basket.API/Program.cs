using Basket.API.Data;

namespace Basket.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCarter();

            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(Program).Assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            
            builder.Services.AddMarten(options =>
            {
                options.Connection(builder.Configuration.GetConnectionString("Database")!);
                options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
            }).UseLightweightSessions();

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("Redis");
            });

            builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
            {
                options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
                };

                return handler;
            });

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();


            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
                .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

            var app = builder.Build();

            app.MapCarter();

            app.UseExceptionHandler(options => { });

            app.UseHealthChecks("/health");

            app.Run();
        }
    }
}
