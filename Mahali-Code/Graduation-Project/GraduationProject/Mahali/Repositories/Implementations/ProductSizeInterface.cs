using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;

namespace Mahali.Repositories.Implementations
{
    public class ProductSizeInterface : GenericInterface<ProductSizes>, IProductSizeInterface
    {
        public ProductSizeInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }
    }
}

