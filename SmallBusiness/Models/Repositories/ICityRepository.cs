using System.Linq;

namespace SmallBusiness.Models.Repositories
{
    public interface ICityRepository
    {
        IQueryable<City> Cities { get; }
    }
}