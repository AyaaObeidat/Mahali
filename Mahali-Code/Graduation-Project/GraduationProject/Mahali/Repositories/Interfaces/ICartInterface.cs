using Mahali.Models;

namespace Mahali.Repositories.Interfaces
{
    public interface ICartInterface : IGenericInterface<Cart>
    {
        public  Task<Cart?> GetByCustomerIdAsync(Guid customerId);
    }
}
