using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mahali.Repositories.Implementations
{
    public class CategoryInterface : GenericInterface<Category>, ICategoryInterface
    {
        public CategoryInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<Category>().Include(x => x.Products).FirstOrDefaultAsync(a => a.Id == id);
        }

        public override async Task<List<Category>?> GetAllAsync()
        {
            return await _dbContext.Set<Category>().Include(x => x.Products).ToListAsync();
        }

    }
}
