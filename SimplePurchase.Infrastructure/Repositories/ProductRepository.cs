using Microsoft.Extensions.Configuration;
using SimpleApplication.Domain.Models;
using SimplePurchase.Application.Interfaces;
using System.Collections.Generic;

namespace SimplePurchase.Infrastructure.Repositories
{
    class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(IConfiguration configuration) : base("Product", configuration.GetConnectionString("DefaultConnection"))
        { }

        public IEnumerable<ProductEntity> GetAllProducts()
        {
            return GetAll<ProductEntity>();
        }
    }
}
