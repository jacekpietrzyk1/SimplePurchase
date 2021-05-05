using SimpleApplication.Domain.Models;
using System.Collections.Generic;

namespace SimplePurchase.Application.Interfaces
{
    public interface ILineItemRepository
    {
        int AddLineItems(IEnumerable<LineItemEntity> lineitems);
    }
}
