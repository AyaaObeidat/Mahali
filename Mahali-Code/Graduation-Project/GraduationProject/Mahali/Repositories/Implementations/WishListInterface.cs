using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mahali.Repositories.Implementations
{
    public class WishListInterface : GenericInterface<WishList>, IWishListInterface
    {
        public WishListInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<WishList?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<WishList>().Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
        }
        public  async Task<WishList?> GetByCustomerIdAsync(Guid customerId)
        {
            return await _dbContext.Set<WishList>().Include(x => x.Products).FirstOrDefaultAsync(x => x.CustomerId == customerId);
        }
    }
}
