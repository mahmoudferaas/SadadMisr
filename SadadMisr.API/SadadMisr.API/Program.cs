using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SadadMisr.DAL;
using SadadMisr.DAL.Context;
using System.Threading.Tasks;

namespace SadadMisr.API
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var env = scope.ServiceProvider.GetService<IWebHostEnvironment>();
                var dbContext = scope.ServiceProvider.GetService<SadadMasrDbContext>();
                dbContext.Database.Migrate();
                await SadadMasrDbContextSeeding.SeedData(dbContext);
            };
            await host.RunAsync();
            return 0;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}