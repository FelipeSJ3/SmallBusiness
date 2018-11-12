using System.Linq;

namespace SmallBusiness.Models.Repositories
{
    public interface IRegionRepository
    {
        IQueryable<Region> Regions { get; }
    }
}