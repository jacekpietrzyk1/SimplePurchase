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
                    $"INSERT INTO {base.GetTableName()} ([Id],[Count],[ProductId],[CreationDate],[IsConfirmed]) " +
                    "VALUES (@Id,@Count,@ProductId,@CreationDate,@IsConfirmed)", new
                    {
                        newPurchase.Id,
                        newPurchase.Count,
                        newPurchase.ProductId,
                        newPurchase.CreationDate,
                        newPurchase.IsConfirmed
                    });
            }
            catch (Exception)
            {
                return -1;
            }

            return status;
        }
    }
}
