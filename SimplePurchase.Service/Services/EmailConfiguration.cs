using SimplePurchase.Service.Interfaces;
using SimplePurchase.Service.Interfaces.Contact;

namespace SimplePurchase.Service.Services
{
    public class EmailConfiguration : IEmailConfiguration
    {

        public string Alias { get; set; }
        public string NoReply { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }

        public string PopServer { get; set; }
        public int PopPort { get; set; }
        public string PopUsername { get; set; }
        public string PopPassword { get; set; }
    }
}
