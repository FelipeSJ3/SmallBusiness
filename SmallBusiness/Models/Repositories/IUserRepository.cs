using System.Linq;

namespace SmallBusiness.Models.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
    }
}