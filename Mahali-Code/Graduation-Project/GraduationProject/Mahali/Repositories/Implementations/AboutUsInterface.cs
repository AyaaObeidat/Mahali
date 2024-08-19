using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;

namespace Mahali.Repositories.Implementations
{
    public class AboutUsInterface : GenericInterface<AboutUs>, IAboutUsInterface
    {
        public AboutUsInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Guid> GetIdAsync()
        {
            var aboutUs = await GetAllAsync();
            return aboutUs.First().Id;
        }
    }
}
