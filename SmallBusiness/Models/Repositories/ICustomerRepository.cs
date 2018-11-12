using System.Linq;

namespace SmallBusiness.Models.Repositories
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> Customers { get; }
    }
}