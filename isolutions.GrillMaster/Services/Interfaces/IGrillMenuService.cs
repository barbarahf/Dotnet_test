using isolutions.GrillMaster.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace isolutions.GrillMaster.Services
{
    public interface IGrillMenuService
    {
        Task<IEnumerable<GrillMenu>> GetGrillMenus();
    }
}
