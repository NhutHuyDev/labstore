using Catalog.API.Data;
using System.Threading.Tasks;

namespace Catalog.API
{
    public class Program
    {
        public static async Task Main(string[] args)
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
            }).UseLightweightSessions();

            if (builder.Environment.IsDevelopment())
                builder.Services.InitializeMartenWith<CatalogInitialData>();

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

            var app = builder.Build();

            app.MapCarter();

            app.UseExceptionHandler(options => { });

            app.UseHealthChecks("/health");

            app.Run();
        }
    }
}
