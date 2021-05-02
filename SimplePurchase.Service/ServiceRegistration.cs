using Microsoft.Extensions.DependencyInjection;
using SimplePurchase.Service.Interfaces;
using SimplePurchase.Service.Interfaces.Contact;
using SimplePurchase.Service.Services;

namespace SimplePurchase.Service
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
