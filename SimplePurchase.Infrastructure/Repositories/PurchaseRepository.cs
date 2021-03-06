using Microsoft.Extensions.Configuration;
using SimpleApplication.Domain.Models;
using SimplePurchase.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
            catch (Exception)
            {
                return -1;
            }

            return status;
        }

        public IEnumerable<PurchaseEntity> GetAllUserPurchases(string userId)
        {
            try
            {
                var result = Query<PurchaseEntity>($"SELECT * FROM {base.GetTableName()} WHERE [UserId] = @userId",
                            new { userId = userId });
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<PurchaseEntity> GetNewPurchases()
        {
            try
            {
                var result = Query<PurchaseEntity>($"SELECT *, CASE WHEN (SELECT COUNT(*) FROM {base.GetTableName()} pi " +
                    $"WHERE pi.[UserId] = p.[UserId] AND pi.[IsProcessed] = 1 AND pi.[IsConfirmed] = 1) > 0 " +
                    $"THEN 0 ELSE 1 END AS IsNewCustomer FROM {base.GetTableName()} p WHERE [IsProcessed] = 0");
                return result;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public decimal GetAveragePurchaseAmount(string userId)
        {
            try
            {
                var result = Query<decimal>($"SELECT SUM(TotalCount)/COUNT(*) FROM {base.GetTableName()} WHERE [UserId] = @userId AND [IsProcessed] = 1",
                            new { userId = userId }).FirstOrDefault();
                return result;

            }
            catch (Exception)
            {
                return Decimal.Zero;
            }
        }

        public int MarkPurchaseAsProcessed(string purchaseId)
        {
            int status = -1;

            try
            {
                status = Execute($"UPDATE {base.GetTableName()} SET [IsProcessed] = 1 WHERE [Id] = @id", new { id = purchaseId });
            }
            catch (Exception)
            {
                return -1;
            }

            return status;
        }

        public int MarkPurchaseAsConfirmed(string purchaseId)
        {
            int status = -1;

            try
            {
                status = Execute($"UPDATE {base.GetTableName()} SET [IsConfirmed] = 1 WHERE [Id] = @id", new { id = purchaseId });
            }
            catch (Exception)
            {
                return -1;
            }

            return status;
        }

        public int MarkPurchaseAsSuspended(string purchaseId)
        {
            int status = -1;

            try
            {
                status = Execute($"UPDATE {base.GetTableName()} SET [IsSuspended] = 1 WHERE Id = @id", new { id = purchaseId });
            }
            catch (Exception)
            {
                return -1;
            }

            return status;
        }
    }
}
