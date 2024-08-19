using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;

namespace Mahali.Repositories.Implementations
{
    public class ProductColorInterface : GenericInterface<ProductColors>, IProductColorInterface
    {
        public ProductColorInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }
    }
}

