using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;

namespace Mahali.Repositories.Implementations
{
    public class ReportInterface : GenericInterface<Report>, IReportInterface
    {
        public ReportInterface (MahaliDbContext dbContext) : base(dbContext)
        {
        }
    }
}

