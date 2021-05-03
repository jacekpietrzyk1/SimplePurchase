using SimpleApplication.Domain.Models;
using System.Collections.Generic;

namespace SimplePurchase.Application.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<ProductEntity> GetAllProducts();
        IEnumerable<ProductEntity> GetPurchaseProducts(int[] productIds);

    }
}
