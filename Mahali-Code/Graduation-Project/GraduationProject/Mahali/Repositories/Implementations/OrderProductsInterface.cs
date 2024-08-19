using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;

namespace Mahali.Repositories.Implementations
{
    public class OrderProductsInterface : GenericInterface<OrderProducts>, IOrderProductsInterface
    {
        public OrderProductsInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }
    }
}

