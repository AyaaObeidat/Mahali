using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;
using MahaliDtos;
using Microsoft.EntityFrameworkCore;

namespace Mahali.Repositories.Implementations
{
    public class LatestProductsVisitedInterface : GenericInterface<LatestProductsVisited>, ILatestProductVisitedInterface
    {
        public LatestProductsVisitedInterface (MahaliDbContext dbContext) : base(dbContext)
        {
        }
        public  async Task<List<LatestProductsVisited>> GetByCustomerIdAsync(Guid customerId)
        {
            return await _dbContext.Set<LatestProductsVisited>().Where(x => x.CustomerId == customerId).ToListAsync();
        }
    }
}

