using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mahali.Repositories.Implementations
{
    public class ShopInterface : GenericInterface<Shop>, IShopInterface
    {
        public ShopInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<Shop?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<Shop>().Include(x => x.Orders).Include(x => x.Reviews).Include(x => x.Products).FirstOrDefaultAsync(a => a.Id == id);
        }
        public override async Task<List<Shop>?> GetAllAsync()
        {
            return await _dbContext.Set<Shop>().Include(x => x.Orders).Include(x => x.Reviews).Include(x => x.Products).ToListAsync();
        }

    }
}
