using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mahali.Repositories.Implementations
{
    public class ProductInterface : GenericInterface<Product>, IProductInterface
    {
        public ProductInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<Product>().Include(x => x.ColorsList).Include(x => x.SizesList).Include(x => x.Reviews).FirstOrDefaultAsync(a => a.Id == id);
        }
        public override async Task<List<Product>?> GetAllAsync()
        {
            return await _dbContext.Set<Product>().Include(x => x.ColorsList).Include(x => x.SizesList).Include(x => x.Carts).Include(x => x.WishLists)
               .Include(x => x.Orders).Include(x => x.Reviews).ToListAsync();
        }
    }
}
