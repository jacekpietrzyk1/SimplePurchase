using SimpleApplication.Domain.Models;
using System.Collections.Generic;

namespace SimplePurchase.Application.Interfaces
{
    public interface IPurchaseRepository
    {
        int AddPurchase(PurchaseEntity newPurchase);

        IEnumerable<PurchaseEntity> GetNewPurchases();

        int MarkPurchaseAsProcessed(string purchaseId);

        int MarkPurchaseAsSuspended(string purchaseId);
    }
}
