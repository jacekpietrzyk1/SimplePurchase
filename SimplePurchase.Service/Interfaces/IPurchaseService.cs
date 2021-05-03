using SimplePurchase.Service.Models.Store;
using System.Collections.Generic;

namespace SimplePurchase.Service.Interfaces
{
    public interface IPurchaseService
    {
        bool AddPurchase(IEnumerable<ProductModel> products, string userId);
    }
}
