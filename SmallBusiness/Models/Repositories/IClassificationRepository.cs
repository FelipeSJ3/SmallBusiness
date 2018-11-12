using System.Linq;

namespace SmallBusiness.Models.Repositories
{
    public interface IClassificationRepository
    {
        IQueryable<Classification> Classifications { get; }
    }
}