using SimplePurchase.Service.Models.Store;
using System.Collections.Generic;

namespace SimplePurchase.Service.Interfaces
{
    public interface IPurchaseService
    {
        bool AddPurchase(IEnumerable<ProductModel> products, string userId);
        IEnumerable<PurchaseModel> GetNewPurchases();

        IEnumerable<PurchaseModel> GetAllUserPurchases(string userId);

        bool SuspendPurchase(string purchaseId);

        bool MarkAsProcessed(string purchaseId);

        bool MarkAsConfirmed(string purchaseId);

        decimal GetAveragePurchaseAmount(string userId);
    }
}
