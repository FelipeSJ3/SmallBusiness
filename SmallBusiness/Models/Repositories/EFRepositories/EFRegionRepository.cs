using System.Linq;

namespace SmallBusiness.Models.Repositories.EFRepositories
{
    public class EFRegionRepository : IRegionRepository
    {
        private ApplicationDbContext context;

        public EFRegionRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Region> Regions => context.Regions;
    }
}