using Microsoft.Extensions.Configuration;
using SimpleApplication.Domain.Models;
using SimplePurchase.Application.Interfaces;
using System;
using System.Collections.Generic;

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

        public IEnumerable<PurchaseEntity> GetNewPurchases()
        {
            try
            {
                var result = Query<PurchaseEntity>($"SELECT *, CASE WHEN (SELECT count(*) FROM {base.GetTableName()} pi " +
                    $"WHERE pi.UserId = p.UserId AND pi.IsProcessed = 1 AND pi.IsConfirmed = 1) > 0 " +
                    $"THEN 0 ELSE 1 END AS IsNewCustomer FROM {base.GetTableName()} p WHERE [IsProcessed] = 0");
                return result;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
