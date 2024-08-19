using Mahali.Models;

namespace Mahali.Repositories.Interfaces
{
    public interface IShopRequestInterface : IGenericInterface<ShopRequest>
    {
        public Task<ShopRequest> GetRequestByShopIdAsync(Guid shopId);
    }

    
}
