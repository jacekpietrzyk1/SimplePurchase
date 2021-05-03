using Microsoft.Extensions.DependencyInjection;
using SimplePurchase.Application.Interfaces;
using SimplePurchase.Infrastructure.Repositories;

namespace SimplePurchase.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IPurchaseRepository, PurchaseRepository>();
            services.AddTransient<ILineItemRepository, LineItemRepository>();
        }
    }
}
