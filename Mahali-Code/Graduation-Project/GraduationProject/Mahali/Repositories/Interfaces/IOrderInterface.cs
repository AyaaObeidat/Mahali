using Mahali.Models;

namespace Mahali.Repositories.Interfaces
{
    public interface IOrderInterface : IGenericInterface<Order>
    {
        public  Task<Order?> GetByIdAsync(int id);
    }
}
