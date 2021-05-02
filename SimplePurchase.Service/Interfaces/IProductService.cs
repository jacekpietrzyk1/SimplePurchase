using SimplePurchase.Service.Models;
using System.Collections.Generic;

namespace SimplePurchase.Service.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductViewModel> GetProducts();
    }
}
