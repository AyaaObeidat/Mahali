using Mahali.Models;

namespace Mahali.Repositories.Interfaces
{
    public interface IWishListInterface : IGenericInterface<WishList>
    {
        public Task<WishList?> GetByCustomerIdAsync(Guid customerId);
    }
}
