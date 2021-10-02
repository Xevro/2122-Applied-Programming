
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MandelbrotFractalApplication.Models
{
    public interface ILogic
    {
        Task<List<DoublePoint>> GetPointsAsync(int count);
    }
}
