using Mahali.Models;

namespace Mahali.Repositories.Interfaces
{
    public interface IAboutUsInterface : IGenericInterface<AboutUs>
    {
        public  Task<Guid> GetIdAsync();
    }
}
