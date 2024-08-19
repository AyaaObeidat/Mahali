using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;

namespace Mahali.Repositories.Implementations
{
    public class CustomerInterface : GenericInterface<Customer>, ICustomerInterface
    {
        public CustomerInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }
    }
}
