using System;

namespace SimpleApplication.Domain.Models
{
    public class PurchaseEntity
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
