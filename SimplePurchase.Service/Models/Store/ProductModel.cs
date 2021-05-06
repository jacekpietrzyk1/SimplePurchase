using System.ComponentModel.DataAnnotations;

namespace SimplePurchase.Service.Models.Store
{
    public class ProductModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(0, 1000000)]
        public int Count { get; set; }
    }
}
