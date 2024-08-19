using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;

namespace Mahali.Repositories.Implementations
{
    public class CartProductsInterface : GenericInterface<CartProducts>, ICartProductsInterface
    {
        public CartProductsInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }
    }
}
