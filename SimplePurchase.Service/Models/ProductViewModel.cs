namespace SimplePurchase.Service.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string ImageLink { get; set; }
        public string ProductLabel => $"{Model} ({Brand})";
    }
}
