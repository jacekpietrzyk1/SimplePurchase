using Microsoft.Extensions.Configuration;
using SimpleApplication.Domain.Models;
using SimplePurchase.Application.Interfaces;
using System;
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

        public IEnumerable<ProductEntity> GetPurchaseProducts(int[] productIds)
        {
            try
            {
                var result = Query<ProductEntity>($"SELECT * FROM {base.GetTableName()} WHERE id IN @ids",
                    new { ids = productIds });
                return result;

            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
