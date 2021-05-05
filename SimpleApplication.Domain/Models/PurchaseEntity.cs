using System;

namespace SimpleApplication.Domain.Models
{
    public class PurchaseEntity
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public decimal Total { get; set; }
        public int TotalCount { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsNewCustomer { get; set; }
    }
}
