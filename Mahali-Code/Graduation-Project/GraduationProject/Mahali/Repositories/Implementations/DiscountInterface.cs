using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;

namespace Mahali.Repositories.Implementations
{
    public class DiscountInterface : GenericInterface<Discount>, IDiscountInterface
    {
        public DiscountInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }
    }
}

