using System.Linq;

namespace SmallBusiness.Models.Repositories.EFRepositories
{
    public class EFCityRepository : ICityRepository
    {
        private ApplicationDbContext context;

        public EFCityRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<City> Cities => context.Cities;
    }
}