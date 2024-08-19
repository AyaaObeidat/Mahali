using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;

namespace Mahali.Repositories.Implementations
{
    public class ShopOrdersInterface : GenericInterface<ShopOrders>, IShopOrdersInterface
    {
        public ShopOrdersInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }
    }
}
