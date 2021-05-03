using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimplePurchase.Service.Interfaces;
using SimplePurchase.Web.Areas.Identity.Data;
using SimplePurchase.Web.Models.Purchase;
using System.Collections.Generic;

namespace SimplePurchase.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<SimplePurchaseWebUser> _userManager;

        public StoreController(IProductService productService,
            UserManager<SimplePurchaseWebUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
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
            var user = User;
        
            return null;
        }
    }
}
