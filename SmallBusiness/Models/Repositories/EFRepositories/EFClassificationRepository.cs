using System.Linq;

namespace SmallBusiness.Models.Repositories.EFRepositories
{
    public class EFClassificationRepository : IClassificationRepository
    {
        private ApplicationDbContext context;

        public EFClassificationRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Classification> Classifications => context.Classifications;
    }
}