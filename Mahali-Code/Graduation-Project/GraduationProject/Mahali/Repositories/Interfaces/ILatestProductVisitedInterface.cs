using Mahali.Models;
using MahaliDtos;

namespace Mahali.Repositories.Interfaces
{
    public interface ILatestProductVisitedInterface : IGenericInterface<LatestProductsVisited>
    {
        public Task<List<LatestProductsVisited>> GetByCustomerIdAsync(Guid customerId);
    }
}
