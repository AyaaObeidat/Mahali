using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mahali.Repositories.Implementations
{
    public class OrderInterface : GenericInterface<Order>, IOrderInterface
    {
        public OrderInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Order>().Include(X => X.Products).FirstOrDefaultAsync(X => X.Id == id);
        }

        public override async Task<List<Order>?> GetAllAsync()
        {
            return await _dbContext.Set<Order>().Include(X => X.Products).ToListAsync();
        }
    }
}
