using Microsoft.Extensions.Configuration;
using SimplePurchase.Service.Interfaces;
using SimplePurchase.Service.Interfaces.Contact;
using SimplePurchase.Service.Models.Contact;
using SimplePurchase.Web.Interfaces;
using System.Collections.Generic;

namespace SimplePurchase.Web.PurchaseMonitor
{
    public class PurchaseMonitoring : IMonitoring
    {
        private readonly IConfiguration _configuration;
        private readonly IPurchaseService _purchaseService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public PurchaseMonitoring(IConfiguration configuration, IPurchaseService purchaseService, IUserService userService, IEmailService emailService)
        {
            _purchaseService = purchaseService;
            _configuration = configuration;
            _userService = userService;
            _emailService = emailService;
        }

        public bool PurchaseMonitoringProcess()
        {
            var newPurchases = _purchaseService.GetNewPurchases();

            if (newPurchases is null)
                return false;

            foreach (var item in newPurchases)
            {
                var country = _userService.GetUserCountry(item.UserId);

                if (item.IsNewCustomer && IsCountryToIgnore(country) && IsCountToIgnore(item.TotalCount))
                {
                    SuspendPurchase(item);

                }
                else if (IsAmountStrange(item.UserId, item.TotalCount))
                {
                    SuspendPurchase(item);
                }
                else
                {
                    ConfirmPurchase(item);
                }
            }

            return true;
        }

        private void ConfirmPurchase(Service.Models.Store.PurchaseModel item)
        {
            _purchaseService.MarkAsProcessed(item.Id);
            var email = _userService.GetUserEmail(item.UserId);
            NotifyAbountPurchaseStatus("Dear User, your purchase has been confirmed.", email);
        }

        private void SuspendPurchase(Service.Models.Store.PurchaseModel item)
        {
            var isSuccess = _purchaseService.SuspendPurchase(item.Id);

            if (isSuccess)
            {
                _purchaseService.MarkAsProcessed(item.Id);
                var email = _userService.GetUserEmail(item.UserId);
                NotifyAbountPurchaseStatus("Dear User, your purchase has been suspended. We will contact you soon.", email);
            }
        }

        private bool IsCountryToIgnore(string country)
        {
            if (string.IsNullOrEmpty(country))
                return false;

            var countryToIgnore = _configuration.GetValue<string>("MonitoringSettings:PurchaseCountry");

            return country == countryToIgnore;
        }

        private bool IsCountToIgnore(int lineItemsCount)
        {
            var countToIgnore = _configuration.GetValue<int>("MonitoringSettings:PurchaseCount");

            return lineItemsCount > countToIgnore;
        }

        private bool IsAmountStrange(string userId, int totalPurchaseAmount)
        {
            var averageAmount = _purchaseService.GetAveragePurchaseAmount(userId);
            return totalPurchaseAmount > 5 * decimal.ToInt32(averageAmount);
        }

        private void NotifyAbountPurchaseStatus(string purchaseStatus, string emailTo)
        {
            if (string.IsNullOrEmpty(emailTo))
                return;

            var message = new EmailMessage()
            {
                ToAddresses = new List<EmailAddress>() { new EmailAddress() { Address = emailTo, Name = emailTo } },
                Subject = "Your Purchase status has been changed",
                Content = purchaseStatus
            };

            _emailService.SendAsync(message);
        }
    }
}
