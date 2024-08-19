using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mahali.Repositories.Implementations
{
    public class ShopReqestInterface : GenericInterface<ShopRequest>, IShopRequestInterface
    {
        public ShopReqestInterface (MahaliDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<ShopRequest?> GetRequestByShopIdAsync(Guid shopId)
        {
            return await _dbContext.Set<ShopRequest>().FirstOrDefaultAsync(x => x.ShopId == shopId);
        }
    }
}

