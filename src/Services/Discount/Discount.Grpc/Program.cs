using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddGrpc(options =>
        {
            options.Interceptors.Add<LoggingGrpcInterceptor>();
        });
        builder.Services.AddDbContext<DiscountContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("Database")));

        var app = builder.Build();

        app.UseMigration();
        app.MapGrpcService<DiscountService>();
        app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


        app.Run();
    }
}