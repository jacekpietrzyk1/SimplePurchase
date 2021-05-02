using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplePurchase.Service.Interfaces;
using SimplePurchase.Web.Models.Purchase;
using System.Collections.Generic;

namespace SimplePurchase.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductService _productService;

        public StoreController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var products = _productService.GetProducts();
            return View(products);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreatePurchase(IEnumerable<ProductRequestModel> products)
        {
            var user = HttpContext.User;
            return null;
        }
    }
}
