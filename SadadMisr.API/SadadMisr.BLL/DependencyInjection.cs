using Microsoft.Extensions.DependencyInjection;
using SadadMisr.BLL.Common;
using SadadMisr.BLL.Services.Implementations;
using SadadMisr.BLL.Services.Interfaces;
using System.Reflection;

namespace SadadMisr.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBLL(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IManifestService, ManifestService>();
            services.AddTransient<IBillService, BillService>();
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }
    }
}