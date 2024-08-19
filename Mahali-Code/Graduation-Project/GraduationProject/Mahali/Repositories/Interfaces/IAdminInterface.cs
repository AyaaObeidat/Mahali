using Mahali.Models;

namespace Mahali.Repositories.Interfaces
{
    public interface IAdminInterface : IGenericInterface<Admin>
    {
        public Task<Guid> GetIdAsync();
    }
}
