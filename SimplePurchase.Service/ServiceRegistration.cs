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
            services.AddTransient<IPurchaseService, PurchaseService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
