using SimpleApplication.Domain.Models;

namespace SimplePurchase.Application.Interfaces
{
    public interface IPurchaseRepository
    {
        public int AddPurchase(PurchaseEntity newPurchase);
    }
}
