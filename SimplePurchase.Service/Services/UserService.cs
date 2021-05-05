using SimplePurchase.Application.Interfaces;
using SimplePurchase.Service.Interfaces;

namespace SimplePurchase.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string GetUserCountry(string userId)
        {
            return _userRepository.GetUserCountry(userId);
        }

        public string GetUserEmail(string userId)
        {
            return _userRepository.GetUserEmail(userId);
        }

    }
}
