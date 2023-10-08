using Nasa.Common.DTO.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.BLL.ServicesContracts
{
    public interface ICoordinatesService
    {
        double ComputeDistance(CoordinatesDto left, CoordinatesDto right);
        CoordinatesDto GetCoordinatesInstance(string coordinates);            
    }
}
