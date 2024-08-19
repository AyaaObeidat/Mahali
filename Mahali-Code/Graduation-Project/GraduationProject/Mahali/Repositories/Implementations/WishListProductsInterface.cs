using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;

namespace Mahali.Repositories.Implementations
{
    public class WishListProductsInterface : GenericInterface<WishListProducts>, IWishListProductsInterface
    {
        public WishListProductsInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }
    }
}
