using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebDB.Models;
/* Program class is responsible for creating the host and running the application.
*/

namespace WebDB.Controllers
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<WebDbContext>();
                services.AddTransient<DatabaseController>();
            });
            var host = builder.Build();

            // Get an instance of DatabaseController
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var controller = services.GetRequiredService<DatabaseController>();
            var dbContext = services.GetRequiredService<WebDbContext>();

        OrderItem.OrderItemDemo(dbContext, 1);
            // Run the host
            await host.RunAsync();
        }
    }
}