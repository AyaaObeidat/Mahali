using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Implementations;
using Mahali.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mahali.Repositories.Implementations
{
    public class CartInterface : GenericInterface<Cart>, ICartInterface
    {
        public CartInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<Cart?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<Cart>().Include(x => x.Products).FirstOrDefaultAsync(x => x.Id==id);
        }
        public  async Task<Cart?> GetByCustomerIdAsync(Guid customerId)
        {
            return await _dbContext.Set<Cart>().Include(x => x.Products).FirstOrDefaultAsync(x => x.CustomerId == customerId);
        }
    }
}
