using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimplePurchase.Service.Interfaces;
using SimplePurchase.Service.Interfaces.Contact;
using SimplePurchase.Service.Models.Contact;
using SimplePurchase.Service.Models.Store;
using SimplePurchase.Web.Areas.Identity.Data;
using SimplePurchase.Web.Models.Store;
using System.Collections.Generic;

namespace SimplePurchase.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<SimplePurchaseWebUser> _userManager;
        private readonly IPurchaseService _purchaseService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public StoreController(IProductService productService,
            IPurchaseService purchaseService,
            UserManager<SimplePurchaseWebUser> userManager,
            IUserService userService,
            IEmailService emailService)
        {
            _productService = productService;
            _purchaseService = purchaseService;
            _userManager = userManager;
            _userService = userService;
            _emailService = emailService;
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
            if (!ModelState.IsValid)
            {
                return BadRequest("You have to order more than 0");
            }

            var userId = _userManager.GetUserId(User);
            var result = _purchaseService.AddPurchase(products, userId);
            if (result)
            {
                var email = _userService.GetUserEmail(userId);
                NotifyPurchaseSubmitted(email);
                return Ok();
            }

            return StatusCode(304);
        }

        [Authorize]
        public IActionResult History()
        {
            var userId = _userManager.GetUserId(User);

            var historyViewModel = new UserHistoryViewModel()
            {
                Purchases = _purchaseService.GetAllUserPurchases(userId),
                AverageAmount = _purchaseService.GetAveragePurchaseAmount(userId)
            };

            return View(historyViewModel);
        }

        private void NotifyPurchaseSubmitted(string emailTo)
        {
            if (string.IsNullOrEmpty(emailTo))
                return;

            var message = new EmailMessage()
            {
                ToAddresses = new List<EmailAddress>() { new EmailAddress() { Address = emailTo, Name = emailTo } },
                Subject = "Your Purchase has been submitted",
                Content = "Congratulations! We have your purchase! You will be notified about status change as soon as possible"
            };

            _emailService.SendAsync(message);
        }
    }
}
