using Microsoft.Extensions.Configuration;
using SimplePurchase.Application.Interfaces;
using System;
using System.Linq;

namespace SimplePurchase.Infrastructure.Repositories
{
    class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base("AspNetUsers", configuration.GetConnectionString("DefaultConnection"))
        { }

        public string GetUserCountry(string userId)
        {
            try
            {
                var result = Query<string>($"SELECT Country FROM {base.GetTableName()} WHERE Id = @userId", new { userId = userId }).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetUserEmail(string userId)
        {
            try
            {
                var result = Query<string>($"SELECT Email FROM {base.GetTableName()} WHERE Id = @userId", new { userId = userId }).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
