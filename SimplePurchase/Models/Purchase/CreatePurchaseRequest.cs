using System.Collections.Generic;

namespace SimplePurchase.Web.Models.Purchase
{
    public class CreatePurchaseRequest
    {
        public IEnumerable<ProductRequestModel> Products { get; set; }
    }
}
