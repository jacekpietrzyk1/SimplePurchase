namespace SimpleApplication.Domain.Models
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string ImageLink { get; set; }
    }
}
