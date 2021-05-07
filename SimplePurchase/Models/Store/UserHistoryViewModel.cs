using SimplePurchase.Service.Models.Store;
using System.Collections.Generic;

namespace SimplePurchase.Web.Models.Store
{
    public class UserHistoryViewModel
    {
        public IEnumerable<PurchaseModel> Purchases { get; set; }
        public decimal AverageAmount { get; set; }

    }
}
