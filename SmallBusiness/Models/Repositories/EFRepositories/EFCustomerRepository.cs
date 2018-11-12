using System.Linq;

namespace SmallBusiness.Models.Repositories.EFRepositories
{
    public class EFCustomerRepository : ICustomerRepository
    {
        private ApplicationDbContext context;

        public EFCustomerRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Customer> Customers => context.Customers;
    }
}