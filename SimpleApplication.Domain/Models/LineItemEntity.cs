namespace SimpleApplication.Domain.Models
{
    public class LineItemEntity
    {
        public int Id { get; set; }
        public string PurchaseId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
