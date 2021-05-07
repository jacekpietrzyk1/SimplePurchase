using SimpleApplication.Domain.Models;
using System.Collections.Generic;

namespace SimplePurchase.Application.Interfaces
{
    public interface IPurchaseRepository
    {
        int AddPurchase(PurchaseEntity newPurchase);

        IEnumerable<PurchaseEntity> GetNewPurchases();

        IEnumerable<PurchaseEntity> GetAllUserPurchases(string userId);

        int MarkPurchaseAsProcessed(string purchaseId);

        int MarkPurchaseAsSuspended(string purchaseId);

        int MarkPurchaseAsConfirmed(string purchaseId);

        decimal GetAveragePurchaseAmount(string userId);
    }
}
