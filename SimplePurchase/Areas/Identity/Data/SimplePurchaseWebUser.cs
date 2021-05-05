using Microsoft.AspNetCore.Identity;

namespace SimplePurchase.Web.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the SimplePurchaseWebUser class
    public class SimplePurchaseWebUser : IdentityUser
    {
        [PersonalData]
        public string Firstname { get; set; }

        [PersonalData]
        public string Lastname { get; set; }

        [PersonalData]
        public string Country { get; set; }
    }
}
