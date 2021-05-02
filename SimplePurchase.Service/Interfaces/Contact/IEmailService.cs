using SimplePurchase.Service.Models.Contact;
using System.Collections.Generic;

namespace SimplePurchase.Service.Interfaces.Contact
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);

        void SendAsync(EmailMessage emailMessage);

        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}
