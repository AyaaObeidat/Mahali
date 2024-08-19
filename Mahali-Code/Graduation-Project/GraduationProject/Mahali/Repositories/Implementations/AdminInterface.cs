using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mahali.Repositories.Implementations
{
    public class AdminInterface : GenericInterface<Admin>, IAdminInterface
    {
        public AdminInterface (MahaliDbContext dbContext) : base(dbContext)
        {
        }

       
        public override async Task<List<Admin>> GetAllAsync()
        {
            return await _dbContext.Set<Admin>().Include(x => x.ShopRequests).Include(x => x.Reports).ToListAsync();
        }

        public override async Task<Admin?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<Admin>().Include(x => x.ShopRequests).Include(x => x.Reports).FirstOrDefaultAsync(x => x.Id==id);
        }
        public async Task<Guid> GetIdAsync()
        {
            var admin = await GetAllAsync();
            return admin.First().Id;  
        }
    }
}

