using System.Linq;

namespace SmallBusiness.Models.Repositories
{
    public interface IGenderRepository
    {
        IQueryable<Gender> Genders { get; }
    }
}