using Microsoft.Extensions.Configuration;
using SimpleApplication.Domain.Models;
using SimplePurchase.Application.Interfaces;
using System;
using System.Collections.Generic;

namespace SimplePurchase.Infrastructure.Repositories
{
    class LineItemRepository : BaseRepository, ILineItemRepository
    {
        public LineItemRepository(IConfiguration configuration) : base("LineItem", configuration.GetConnectionString("DefaultConnection"))
        { }

        public int AddLineItems(IEnumerable<LineItemEntity> lineitems)
        {
            int status = 0;

            try
            {
                foreach (var item in lineitems)
                {
                    var result = Execute(
                    $"INSERT INTO {base.GetTableName()} ([PurchaseId],[ProductId],[Price],[Count]) " +
                    "VALUES (@PurchaseId,@ProductId, @Price, @Count)", new
                    {
                        item.PurchaseId,
                        item.ProductId,
                        item.Price,
                        item.Count
                    });

                    status += result;
                }
            }
            catch (Exception)
            {
                return -1;
            }

            return status;
        }
    }
}
