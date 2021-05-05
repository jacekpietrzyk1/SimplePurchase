namespace SimplePurchase.Service.Interfaces
{
    public interface IUserService
    {
        string GetUserCountry(string userId);

        string GetUserEmail(string userId);
    }
}
