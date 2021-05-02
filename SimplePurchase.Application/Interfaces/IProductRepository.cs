using SimpleApplication.Domain.Models;
using System.Collections.Generic;

namespace SimplePurchase.Application.Interfaces
{
    public interface IProductRepository
    {
        public IEnumerable<ProductEntity> GetAllProducts();
    }
}
