using System.Linq;

namespace SmallBusiness.Models.Repositories.EFRepositories
{
    public class EFGenderRepository : IGenderRepository
    {
        private ApplicationDbContext context;

        public EFGenderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Gender> Genders => context.Genders;
    }
}