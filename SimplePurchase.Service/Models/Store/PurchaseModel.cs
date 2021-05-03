using System;

namespace SimplePurchase.Service.Models.Store
{
    public class PurchaseModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public decimal Total { get; set; }
        public int TotalCount { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsProcessed { get; set; }
    }

}
