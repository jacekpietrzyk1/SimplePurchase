using SimplePurchase.Service.Interfaces;
using SimplePurchase.Web.Interfaces;
using System.Linq;

namespace SimplePurchase.Web.PurchaseMonitor
{
    public class PurchaseMonitor : IMonitor
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseMonitor(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        public bool PurchaseMonitorSystem()
        {
            var newPurchases = _purchaseService.GetNewPurchases();

            if (newPurchases is null)
                return false;

            return newPurchases.Any();
        }
    }
}
