using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimplePurchase.Service.Interfaces;
using SimplePurchase.Service.Models.Store;
using SimplePurchase.Web.Areas.Identity.Data;
using System.Collections.Generic;
using System.Linq;

namespace SimplePurchase.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<SimplePurchaseWebUser> _userManager;
        private readonly IPurchaseService _purchaseService;
        public StoreController(IProductService productService,
            IPurchaseService purchaseService,
            UserManager<SimplePurchaseWebUser> userManager)
        {
            _productService = productService;
            _purchaseService = purchaseService;
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
        public IActionResult CreatePurchase(IEnumerable<ProductModel> products)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("You have to order more than 0");
            }

            var userId = _userManager.GetUserId(User);
            var result = _purchaseService.AddPurchase(products, userId);
            if(result)
                return Ok();

            return StatusCode(304);
        }
    }
}
