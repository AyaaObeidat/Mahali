using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;

namespace Mahali.Repositories.Implementations
{
    public class LocationInterface : GenericInterface<Location>, ILocationInterface
    {
        public LocationInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }
    }
}

