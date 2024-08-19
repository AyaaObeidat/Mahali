using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Interfaces;

namespace Mahali.Repositories.Implementations
{
    public class ReviewRequestInterface : GenericInterface<ReviewRequest>, IReviewRequestInterface
    {
        public ReviewRequestInterface(MahaliDbContext dbContext) : base(dbContext)
        {
        }
    }
}

