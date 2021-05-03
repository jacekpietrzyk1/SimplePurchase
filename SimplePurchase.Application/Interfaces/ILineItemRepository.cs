using SimpleApplication.Domain.Models;
using System.Collections.Generic;

namespace SimplePurchase.Application.Interfaces
{
    public interface ILineItemRepository
    {
        public int AddLineItems(IEnumerable<LineItemEntity> lineitems);
    }
}
