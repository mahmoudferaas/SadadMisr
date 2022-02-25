
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace SadadMisr.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SadadMasrDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbConnection")).EnableSensitiveDataLogging());

            services.AddScoped<ISadadMasrDbContext>(provider => provider.GetService<SadadMasrDbContext>());

            return services;
        }
    }
}
