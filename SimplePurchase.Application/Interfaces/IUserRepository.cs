namespace SimplePurchase.Application.Interfaces
{
    public interface IUserRepository
    {
        string GetUserCountry(string userId);

        string GetUserEmail(string userId);

    }
}
