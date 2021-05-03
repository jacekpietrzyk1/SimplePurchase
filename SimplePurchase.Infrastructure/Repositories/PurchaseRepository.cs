using Microsoft.Extensions.Configuration;
using SimpleApplication.Domain.Models;
using SimplePurchase.Application.Interfaces;
using System;

namespace SimplePurchase.Infrastructure.Repositories
{
    class PurchaseRepository : BaseRepository, IPurchaseRepository
    {
        public PurchaseRepository(IConfiguration configuration) : base("Purchase", configuration.GetConnectionString("DefaultConnection"))
        { }

        public int AddPurchase(PurchaseEntity newPurchase)
        {
            int status = -1;

            try
            {
                status = Execute(
                    $"INSERT INTO {base.GetTableName()} ([Id],[UserId],[Total],[TotalCount],[CreationDate],[IsConfirmed],[IsProcessed]) " +
                    "VALUES (@Id,@UserId, @Total, @TotalCount, @CreationDate,@IsConfirmed, @IsProcessed)", new
                    {
                        newPurchase.Id,
                        newPurchase.UserId,
                        newPurchase.Total,
                        newPurchase.TotalCount,
                        newPurchase.CreationDate,
                        newPurchase.IsConfirmed,
                        newPurchase.IsProcessed
                    });
            }
            catch (Exception ex)
            {
                return -1;
            }

            return status;
        }
    }
}
