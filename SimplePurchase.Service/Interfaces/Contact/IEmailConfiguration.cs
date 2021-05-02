namespace SimplePurchase.Service.Interfaces.Contact
{
    public interface IEmailConfiguration
    {
        string Alias { get; }
        string NoReply { get; }
        string SmtpServer { get; }
        int SmtpPort { get; }
        string SmtpUsername { get; set; }
        string SmtpPassword { get; set; }

        string PopServer { get; }
        int PopPort { get; }
        string PopUsername { get; }
        string PopPassword { get; }
    }
}
